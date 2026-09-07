# Emotions & Care — Backend

**API REST en ASP.NET Core para una app de acompañamiento emocional.** Pacientes y especialistas se vinculan, el diario y los cuestionarios miden progreso, y ese progreso se vuelve visible: un jardín que crece cuando la persona se cuida.

Este repositorio es el backend de ese producto. No es un CRUD genérico: modela un dominio clínico-gamificado (vinculación, adherencia, recompensas) y lo expone a una app móvil.

---

## Por qué este proyecto

Un reclutador ve decenas de APIs con “usuarios y citas”. Aquí el problema de negocio es otro: **cómo sostener el hábito de cuidado sin que se sienta como una planilla**.

La respuesta que construimos es un motor de progreso:

1. El paciente actúa (escribe en el diario, completa un test, se vincula con su especialista, sigue una recomendación).
2. El servidor evalúa si cumplió los criterios de su etapa actual.
3. Si sí, la flor avanza de etapa y llega un push: *“¡Tu flor ha crecido!”*
4. Las primeras veces de cada hábito desbloquean un **logro** y un **sticker** coleccionable.

Eso exige reglas de dominio, no solo endpoints. El resto de este README entra en cómo está resuelto.

---

## Stack

| Capa | Tecnología |
| --- | --- |
| API | ASP.NET Core 6 (`net6.0`) |
| Persistencia | Entity Framework Core 6 + SQL Server (Azure SQL) |
| Push | Firebase Admin SDK (`FirebaseMessaging`) |
| Estilo | REST, JSON, CORS abierto para el cliente móvil |
| Inyección de dependencias | Contenedor nativo de ASP.NET (`AddScoped` / `AddSingleton`) |

---

## Arquitectura

Cuatro proyectos, cada uno con una responsabilidad clara. El flujo de una request es lineal y fácil de seguir:

```
Cliente móvil
    → Controllers  (emotionsandcareproject)
        → Services (Business)
            → Repositories (Data / EF Core)
                → SQL Server
    ← Domain (entidades compartidas, sin I/O)
```

```
emotions_and_care_project/
├── Domain/                      Entidades, enums y modelos de respuesta
├── Data/                        DbContext, repositorios, migraciones EF
│   ├── contracts/               Interfaces de persistencia
│   ├── implementations/         Acceso a datos
│   ├── helpers/                 Constantes de conexión
│   └── Migrations/
├── Business/                    Reglas de negocio
│   ├── contracts/
│   └── implementations/
└── emotionsandcareproject/      Host HTTP: Program.cs, controllers, push
```

**Por qué esta separación.** Domain no conoce EF ni HTTP. Data no decide si un paciente “puede crecer”: solo ejecuta consultas y persiste. Business orquesta (validar, premiar, avanzar etapa). Los controllers traducen eso a status codes y, cuando aplica, disparan un push.

El registro de servicios vive en `Program.cs`: cada `I*Service` se cablea con su implementación y su repositorio. El patrón se repite en todo el módulo (paciente, especialista, citas, notas, cartas, logros, etapas…).

---

## El jardín: cómo crece la planta

Esta es la pieza que más nos importó diseñar. Una flor no sube de nivel porque el cliente lo pida: **sube cuando el backend demuestra que el paciente cumplió los requisitos de su etapa**.

### Modelo

- Cada paciente tiene un `Progress` (`stage`, `lastDate`, `begginDate`).
- Cada etapa del jardín es un `Stage` con una lista de `StageRequest`: nombre del criterio, `value` (umbral) y `dayRange` (ventana en días).
- La flor del paciente (`UserFlower`) tiene un `state` alineado con `EtapaFlor`: semilla → cinco pasos de crecimiento.
- El jardín de la UI (`UserInterface`) admite **hasta 3 flores activas** por posición. Al registrarse, el paciente recibe 4 flores; elige cuáles plantar.

```mermaid
flowchart TD
  A[GET /api/Paciente/{id}/canGrowFlower] --> B[Criterios de la etapa actual]
  B --> C{¿Todos los StageRequest dan true?}
  C -->|No| D[200 false — la flor no crece]
  C -->|Sí| E[Incrementar Progress.stage]
  E --> F[Actualizar lastDate]
  F --> G[Push Firebase: jardín / canGrow]
  G --> H[200 true — el cliente anima la flor]
```

### El truco: un mapa de funciones, no un `switch` eterno

Los requisitos de cada etapa **viven en base de datos**. El código no hardcodea “la etapa 2 pide 1 nota y un test”. Lo que hace es:

1. Leer `stages[progress.stage].stageRequests`.
2. Para cada request, buscar su validador por **nombre**.
3. Ejecutarlo con `(value, dayRange, idPatient)`.
4. Si uno falla, la flor no crece. Si todos pasan, avanza.

Eso está implementado como un **registro de estrategias**:

```csharp
Dictionary<string, Func<int?, int?, int, bool>> progressFunctionMap
```

| Clave en `StageRequest.name` | Qué demuestra el paciente |
| --- | --- |
| `patientRegister` | Existe en el sistema |
| `tutorialCompleted` | `registerState == "registerSuccess"` |
| `firstTestComplete` | Al menos un cuestionario completado |
| `firstDiary` | Al menos una nota en el diario |
| `diary` | *N* notas **por día** durante `dayRange` días (hábito, no un one-shot) |
| `oneRecommendation` | Al menos `value` recomendaciones cumplidas |
| `relateSpecialist` | Ya está vinculado a un especialista |

Agregar un criterio nuevo es: escribir una función `validateX` y registrarla en el mapa. Cambiar umbrales o ventanas de una etapa es **dato**, no deploy de lógica.

`diary` es el validador más exigente: agrupa notas por día calendario y exige consistencia en toda la ventana. No basta con escribir siete notas el domingo.

### Cooldown y notificación

`reviewCanCheck` mira `Progress.lastDate` y propone una ventana de **7 días** entre revisiones (o “nunca revisado” si `begginDate == lastDate`). Cuando el crecimiento ocurre, `updateLastProgressDate` sella el momento.

Si `canGrowFlower` resulta verdadero, el controller no solo persiste: envía un push con payload navegable (`module: yard`, `type: canGrow`) para que la app abra el jardín.

`POST /api/Paciente/{id}/growStage` existe como avance explícito; el camino de producto es el GET de evaluación, que crece y notifica en el mismo flujo.

---

## Recompensas: logros y stickers

El jardín premia **constancia**. Los logros premian **la primera vez** que alguien se atreve a usar una feature. Son dos sistemas a propósito: uno de largo plazo, otro de onboarding.

Cuando el cliente manda `isFirstTime = true` en la acción, el servicio:

1. Persiste la acción (nota, carta, cita, cuestionario…).
2. Asigna un `Goal` al paciente (`AddGoalPatient`).
3. Entrega un `Sticker` coleccionable (`addStickerToPatient`).
4. Devuelve un wrapper (`GoalWithNote`, `GoalWithCart`, `goalWithDate`, `GoalWithTestInfoModel`) para que la UI muestre la recompensa **en el mismo response**, sin un segundo round-trip.

Si el logro o el sticker fallan en el diario, se **revierte la nota**. Preferimos no dejar progreso huérfano.

| Primera acción | Goal id | Sticker id | Tipo de hábito (`GoalType`) |
| --- | --- | --- | --- |
| Enviar una carta a la comunidad | 1 | 1 | `community` |
| Responder una carta | 2 | 2 | `community` |
| Escribir la primera nota del diario | 3 | 3 | (diario / emoción) |
| Agendar la primera cita | 4 | 4 | `specialistFollow` |
| Completar el primer cuestionario | 5 | 5 | `testFollow` |

El diario no es texto suelto: cada `Note` lleva una `Emotion`. El jardín y los logros se alimentan de ese registro emocional, no de un contador abstracto.

Los stickers y las flores se colocan en la interfaz del paciente (`putStickeriInInterface`, `putFlowerInInterface`): si otra pieza ocupa la posición, se desalojan. El jardín es un inventario espacial, no una lista.

---

## Dominio que cubre la API

La API no es un recurso único. Es el backend de un producto de cuidado con dos roles:

**Paciente.** Registro, diario con emociones, cuestionarios (asignación, historial, visibilidad hacia el especialista), recomendaciones por área (sueño, alimentación, relajación, actividad física, vida social), cartas de comunidad, citas, jardín, logros, notificaciones y tema visual.

**Especialista.** Perfil profesional (cédula, enfoque, institución), bandeja de solicitudes de vinculación, pacientes a cargo, agenda y confirmación/rechazo de citas.

**Vinculación.** El paciente usa el `relationalToken` del especialista. Hay dos caminos: solicitud (`vincularEspecialista` + push de “nueva solicitud”) o vínculo directo (`vincularDirecto` + push de sync). El especialista acepta o rechaza.

**Auth de producto.** `POST /api/Usuario/{email}/multiLogin/{password}` resuelve en un solo endpoint si el mail es paciente o especialista. `refreshToken` actualiza el token de dispositivo para Firebase.

**Push.** Firebase Cloud Messaging, con `data` tipado por módulo (`patientRequest`, `sync`, `yard`, etc.) para que la app navegue al lugar correcto, no solo muestre un toast.

---

## Mapa de endpoints

Base: `/api/{Controller}`. Los nombres de controller están en español porque el cliente y el dominio también lo están.

| Controller | Prefijo | Responsabilidad |
| --- | --- | --- |
| `PacienteController` | `/api/Paciente` | CRUD paciente, vinculación, jardín, UI, tema |
| `EspecialistaController` | `/api/Especialista` | CRUD, solicitudes, pacientes, citas del especialista |
| `UsuarioController` | `/api/Usuario` | Login unificado y refresh de token FCM |
| `NotaController` | `/api/Nota` | Diario emocional |
| `CuestionarioController` | `/api/Cuestionario` | Tests, completar, historial, visibilidad |
| `CitaController` | `/api/Cita` | Agenda paciente ↔ especialista |
| `CartaController` | `/api/Carta` | Comunidad (cartas y respuestas) |
| `RecomendacionController` | `/api/Recomendacion` | Catálogo y “recomendación cumplida” |
| `LogroController` | `/api/Logro` | Catálogo de goals |
| `ItemsController` | `/api/Items` | Flores y stickers |
| `StageController` | `/api/Stage` | Etapas y criterios del jardín |
| `NotificacionController` | `/api/Notificacion` | In-app + push |
| `EmocionController` | `/api/Emocion` | Catálogo de emociones |
| `TerminosController` | `/api/Terminos` | Términos y condiciones |

Endpoints que conviene mirar primero si se evalúa el repo:

```
GET    /api/Paciente/{id}/canGrowFlower
POST   /api/Paciente/{id}/growStage
PUT    /api/Paciente/{id}/putFlowerInInterface/{idFlower}/{position}
PUT    /api/Paciente/{id}/putStickeriInInterface/{idUserSticker}/{position}

POST   /api/Nota/{idPaciente}/AgregarNota/{isFirstTime}
POST   /api/Cuestionario/{idPaciente}/completarCuestionario/{idCuestionario}/{isFirstTime}
POST   /api/Carta/{idUsuario}/AgregarCarta/{isPatient}/{isFirtTime}
POST   /api/Cita/{idPaciente}/AgregarCita/{idEspecialista}/{isFirtTime}

POST   /api/Paciente/{id}/vincularEspecialista/{tokenEspecialista}
POST   /api/Especialista/{id}/aceptarSolicitud/{pacientId}
POST   /api/Usuario/{email}/multiLogin/{password}
```

Contrato típico de respuesta premiada: `{ goal, noteId }` (o `cartId` / `dateId` / `TestInfoModel`). `goal` es `null` si no era la primera vez.

---

## Cómo correrlo

Requisitos: .NET 6 SDK y un SQL Server (local o Azure) con la connection string en `emotionsandcareproject/appsettings.json` (`DefaultConnection`).

```bash
dotnet restore emotionsandcareproject/emotionsandcareproject.sln
dotnet run --project emotionsandcareproject/emotionsandcareproject.csproj
```

Por defecto: `https://localhost:7120` y `http://localhost:5092`.

Las migraciones de EF están en `Data/Migrations`. El host registra CORS `AllowAnyOrigin` / método / header para desarrollo con el cliente móvil.

Firebase se inicializa al arrancar (`FirebaseApp.Create`). Sin credenciales válidas, el resto de la API funciona; fallan los pushes.

---

## Decisiones que importan

- **Criterios de crecimiento como datos.** Producto puede endurecer o relajar una etapa sin reescribir C#, mientras el validador exista en el mapa.
- **Estrategia por nombre, no por `if (stage == 2)`.** El código escala con tipos de evidencia (diario, test, vínculo), no con el número de etapas.
- **Dos economías de recompensa.** Stickers = dopamina de onboarding. Flor = adherencia en el tiempo. Mezclarlas en un solo XP habría aplastado esa diferencia.
- **La UI del jardín es servidor.** Posiciones, flores activas y tema viven en `UserInterface`; el cliente no es la fuente de verdad del inventario.
- **Push con intención de navegación.** El payload dice *adónde ir*, no solo *qué pasó*.
- **Capas + contratos.** Cada módulo (notas, citas, cartas…) sigue el mismo recorte: controller → service → repository. Un desarrollador nuevo copia el patrón, no adivina uno distinto por feature.

Si solo vas a leer una clase: `Data/implementations/paciente_repository.cs` (`progressFunctionMap`, `canGrowFlower`, `validatediaryforDays`). Si solo vas a leer un flujo de producto: `Business/implementations/patient_service.cs` + `PacienteController.canGrowFlower`.
