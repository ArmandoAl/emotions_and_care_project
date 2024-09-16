using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfuguracionesPaciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificacionesActivas = table.Column<bool>(type: "bit", nullable: false),
                    DirioActivado = table.Column<bool>(type: "bit", nullable: false),
                    ProgresoActivado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfuguracionesPaciente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuestionarios",
                columns: table => new
                {
                    IdCuestionario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCuestionario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Objetivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instrucciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuestionarios", x => x.IdCuestionario);
                });

            migrationBuilder.CreateTable(
                name: "Emociones",
                columns: table => new
                {
                    IdEmocion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emociones", x => x.IdEmocion);
                });

            migrationBuilder.CreateTable(
                name: "Flores",
                columns: table => new
                {
                    IdFlor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flores", x => x.IdFlor);
                });

            migrationBuilder.CreateTable(
                name: "Logros",
                columns: table => new
                {
                    IdLogro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    idSticker = table.Column<int>(type: "int", nullable: true),
                    idFlor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logros", x => x.IdLogro);
                });

            migrationBuilder.CreateTable(
                name: "Publicaciones",
                columns: table => new
                {
                    IdPublicacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicaciones", x => x.IdPublicacion);
                });

            migrationBuilder.CreateTable(
                name: "Recomendaciones",
                columns: table => new
                {
                    IdRecomendacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    SubTitulo = table.Column<int>(type: "int", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendaciones", x => x.IdRecomendacion);
                });

            migrationBuilder.CreateTable(
                name: "Sticker",
                columns: table => new
                {
                    IdSticker = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sticker", x => x.IdSticker);
                });

            migrationBuilder.CreateTable(
                name: "TerminosYCondiciones",
                columns: table => new
                {
                    IdTerminosYCondiciones = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    terminosYCondiciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminosYCondiciones", x => x.IdTerminosYCondiciones);
                });

            migrationBuilder.CreateTable(
                name: "Pregunta",
                columns: table => new
                {
                    IdPregunta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enunciado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CuestionarioIdCuestionario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregunta", x => x.IdPregunta);
                    table.ForeignKey(
                        name: "FK_Pregunta_Cuestionarios_CuestionarioIdCuestionario",
                        column: x => x.CuestionarioIdCuestionario,
                        principalTable: "Cuestionarios",
                        principalColumn: "IdCuestionario");
                });

            migrationBuilder.CreateTable(
                name: "ImageModel",
                columns: table => new
                {
                    IdImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlorIdFlor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModel", x => x.IdImage);
                    table.ForeignKey(
                        name: "FK_ImageModel_Flores_FlorIdFlor",
                        column: x => x.FlorIdFlor,
                        principalTable: "Flores",
                        principalColumn: "IdFlor");
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    IdComentario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaComentario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublicacionIdPublicacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.IdComentario);
                    table.ForeignKey(
                        name: "FK_Comentario_Publicaciones_PublicacionIdPublicacion",
                        column: x => x.PublicacionIdPublicacion,
                        principalTable: "Publicaciones",
                        principalColumn: "IdPublicacion");
                });

            migrationBuilder.CreateTable(
                name: "RecomendacionCompletada",
                columns: table => new
                {
                    IdRecomendacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    FechaCompletada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecomendacionIdRecomendacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecomendacionCompletada", x => x.IdRecomendacion);
                    table.ForeignKey(
                        name: "FK_RecomendacionCompletada_Recomendaciones_RecomendacionIdRecomendacion",
                        column: x => x.RecomendacionIdRecomendacion,
                        principalTable: "Recomendaciones",
                        principalColumn: "IdRecomendacion");
                });

            migrationBuilder.CreateTable(
                name: "Especialistas",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CedulaProfesional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enfoque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Institucion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Presentacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicaion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenRelacional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerminosycondicionesIdTerminosYCondiciones = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialistas", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Especialistas_TerminosYCondiciones_TerminosycondicionesIdTerminosYCondiciones",
                        column: x => x.TerminosycondicionesIdTerminosYCondiciones,
                        principalTable: "TerminosYCondiciones",
                        principalColumn: "IdTerminosYCondiciones",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respuesta",
                columns: table => new
                {
                    IdRespuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextoRespuesta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreguntaIdPregunta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuesta", x => x.IdRespuesta);
                    table.ForeignKey(
                        name: "FK_Respuesta_Pregunta_PreguntaIdPregunta",
                        column: x => x.PreguntaIdPregunta,
                        principalTable: "Pregunta",
                        principalColumn: "IdPregunta");
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspecialistaIdUsuario = table.Column<int>(type: "int", nullable: true),
                    ConfiguracionId = table.Column<int>(type: "int", nullable: false),
                    registerSet = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenRelacional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerminosycondicionesIdTerminosYCondiciones = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Pacientes_ConfuguracionesPaciente_ConfiguracionId",
                        column: x => x.ConfiguracionId,
                        principalTable: "ConfuguracionesPaciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pacientes_Especialistas_EspecialistaIdUsuario",
                        column: x => x.EspecialistaIdUsuario,
                        principalTable: "Especialistas",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_Pacientes_TerminosYCondiciones_TerminosycondicionesIdTerminosYCondiciones",
                        column: x => x.TerminosycondicionesIdTerminosYCondiciones,
                        principalTable: "TerminosYCondiciones",
                        principalColumn: "IdTerminosYCondiciones",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cartas",
                columns: table => new
                {
                    IdCarta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmisor = table.Column<int>(type: "int", nullable: false),
                    inicialEmisor = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EspecialistaIdUsuario = table.Column<int>(type: "int", nullable: true),
                    PacienteIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartas", x => x.IdCarta);
                    table.ForeignKey(
                        name: "FK_Cartas_Especialistas_EspecialistaIdUsuario",
                        column: x => x.EspecialistaIdUsuario,
                        principalTable: "Especialistas",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_Cartas_Pacientes_PacienteIdUsuario",
                        column: x => x.PacienteIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmadaPorPaciente = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmadaPorEspecialista = table.Column<bool>(type: "bit", nullable: false),
                    Realizada = table.Column<bool>(type: "bit", nullable: false),
                    PacienteIdUsuario = table.Column<int>(type: "int", nullable: true),
                    EspecialistaIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Citas_Especialistas_EspecialistaIdUsuario",
                        column: x => x.EspecialistaIdUsuario,
                        principalTable: "Especialistas",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_Citas_Pacientes_PacienteIdUsuario",
                        column: x => x.PacienteIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "CuestionarioCompletados",
                columns: table => new
                {
                    IdCuestionarioCompletado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    CuestionarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuestionarioCompletados", x => x.IdCuestionarioCompletado);
                    table.ForeignKey(
                        name: "FK_CuestionarioCompletados_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuestionarioPaciente",
                columns: table => new
                {
                    CuestionariosIdCuestionario = table.Column<int>(type: "int", nullable: false),
                    PacienteIdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuestionarioPaciente", x => new { x.CuestionariosIdCuestionario, x.PacienteIdUsuario });
                    table.ForeignKey(
                        name: "FK_CuestionarioPaciente_Cuestionarios_CuestionariosIdCuestionario",
                        column: x => x.CuestionariosIdCuestionario,
                        principalTable: "Cuestionarios",
                        principalColumn: "IdCuestionario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuestionarioPaciente_Pacientes_PacienteIdUsuario",
                        column: x => x.PacienteIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FloresDelUsuario",
                columns: table => new
                {
                    StickerIdSticker = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlorIdFlor = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Etapa = table.Column<int>(type: "int", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    PacienteIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloresDelUsuario", x => x.StickerIdSticker);
                    table.ForeignKey(
                        name: "FK_FloresDelUsuario_Flores_FlorIdFlor",
                        column: x => x.FlorIdFlor,
                        principalTable: "Flores",
                        principalColumn: "IdFlor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FloresDelUsuario_Pacientes_PacienteIdUsuario",
                        column: x => x.PacienteIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "HistorialesCuestionariosCompletados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCuestionario = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacienteIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialesCuestionariosCompletados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialesCuestionariosCompletados_Pacientes_PacienteIdUsuario",
                        column: x => x.PacienteIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "LogroPaciente",
                columns: table => new
                {
                    PacientesIdUsuario = table.Column<int>(type: "int", nullable: false),
                    logrosIdLogro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogroPaciente", x => new { x.PacientesIdUsuario, x.logrosIdLogro });
                    table.ForeignKey(
                        name: "FK_LogroPaciente_Logros_logrosIdLogro",
                        column: x => x.logrosIdLogro,
                        principalTable: "Logros",
                        principalColumn: "IdLogro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogroPaciente_Pacientes_PacientesIdUsuario",
                        column: x => x.PacientesIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    IdNota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmocionIdEmocion = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    PacienteIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.IdNota);
                    table.ForeignKey(
                        name: "FK_Notas_Emociones_EmocionIdEmocion",
                        column: x => x.EmocionIdEmocion,
                        principalTable: "Emociones",
                        principalColumn: "IdEmocion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notas_Pacientes_PacienteIdUsuario",
                        column: x => x.PacienteIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    IdNotificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoNotificacion = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idRecomandacion = table.Column<int>(type: "int", nullable: true),
                    TipoRecomendacion = table.Column<int>(type: "int", nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PacienteIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.IdNotificacion);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Pacientes_PacienteIdUsuario",
                        column: x => x.PacienteIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesPaciente",
                columns: table => new
                {
                    IdSolicitudPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteIdUsuario = table.Column<int>(type: "int", nullable: false),
                    EspecialistaIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesPaciente", x => x.IdSolicitudPaciente);
                    table.ForeignKey(
                        name: "FK_SolicitudesPaciente_Especialistas_EspecialistaIdUsuario",
                        column: x => x.EspecialistaIdUsuario,
                        principalTable: "Especialistas",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_SolicitudesPaciente_Pacientes_PacienteIdUsuario",
                        column: x => x.PacienteIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StickersDeUsuario",
                columns: table => new
                {
                    IdStickerDeUsuarioModel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StickerIdSticker = table.Column<int>(type: "int", nullable: false),
                    Posicion = table.Column<int>(type: "int", nullable: true),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    PacienteIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StickersDeUsuario", x => x.IdStickerDeUsuarioModel);
                    table.ForeignKey(
                        name: "FK_StickersDeUsuario_Pacientes_PacienteIdUsuario",
                        column: x => x.PacienteIdUsuario,
                        principalTable: "Pacientes",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_StickersDeUsuario_Sticker_StickerIdSticker",
                        column: x => x.StickerIdSticker,
                        principalTable: "Sticker",
                        principalColumn: "IdSticker",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestaCarta",
                columns: table => new
                {
                    IdCarta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReceptor = table.Column<int>(type: "int", nullable: false),
                    LetraReceptor = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSticker = table.Column<int>(type: "int", nullable: true),
                    Leida = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CartaIdCarta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestaCarta", x => x.IdCarta);
                    table.ForeignKey(
                        name: "FK_RespuestaCarta_Cartas_CartaIdCarta",
                        column: x => x.CartaIdCarta,
                        principalTable: "Cartas",
                        principalColumn: "IdCarta");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesCita",
                columns: table => new
                {
                    IdSolicitudCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitaIdCita = table.Column<int>(type: "int", nullable: true),
                    EspecialistaIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesCita", x => x.IdSolicitudCita);
                    table.ForeignKey(
                        name: "FK_SolicitudesCita_Citas_CitaIdCita",
                        column: x => x.CitaIdCita,
                        principalTable: "Citas",
                        principalColumn: "IdCita");
                    table.ForeignKey(
                        name: "FK_SolicitudesCita_Especialistas_EspecialistaIdUsuario",
                        column: x => x.EspecialistaIdUsuario,
                        principalTable: "Especialistas",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "TestInfoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    HistoryTestModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestInfoModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestInfoModel_HistorialesCuestionariosCompletados_HistoryTestModelId",
                        column: x => x.HistoryTestModelId,
                        principalTable: "HistorialesCuestionariosCompletados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestQuestionWithAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestInfoModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestionWithAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestionWithAnswer_TestInfoModel_TestInfoModelId",
                        column: x => x.TestInfoModelId,
                        principalTable: "TestInfoModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartas_EspecialistaIdUsuario",
                table: "Cartas",
                column: "EspecialistaIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Cartas_PacienteIdUsuario",
                table: "Cartas",
                column: "PacienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_EspecialistaIdUsuario",
                table: "Citas",
                column: "EspecialistaIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PacienteIdUsuario",
                table: "Citas",
                column: "PacienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_PublicacionIdPublicacion",
                table: "Comentario",
                column: "PublicacionIdPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_CuestionarioCompletados_PacienteId",
                table: "CuestionarioCompletados",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CuestionarioPaciente_PacienteIdUsuario",
                table: "CuestionarioPaciente",
                column: "PacienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Especialistas_TerminosycondicionesIdTerminosYCondiciones",
                table: "Especialistas",
                column: "TerminosycondicionesIdTerminosYCondiciones");

            migrationBuilder.CreateIndex(
                name: "IX_FloresDelUsuario_FlorIdFlor",
                table: "FloresDelUsuario",
                column: "FlorIdFlor");

            migrationBuilder.CreateIndex(
                name: "IX_FloresDelUsuario_PacienteIdUsuario",
                table: "FloresDelUsuario",
                column: "PacienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialesCuestionariosCompletados_PacienteIdUsuario",
                table: "HistorialesCuestionariosCompletados",
                column: "PacienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ImageModel_FlorIdFlor",
                table: "ImageModel",
                column: "FlorIdFlor");

            migrationBuilder.CreateIndex(
                name: "IX_LogroPaciente_logrosIdLogro",
                table: "LogroPaciente",
                column: "logrosIdLogro");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_EmocionIdEmocion",
                table: "Notas",
                column: "EmocionIdEmocion");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_PacienteIdUsuario",
                table: "Notas",
                column: "PacienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_PacienteIdUsuario",
                table: "Notificaciones",
                column: "PacienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_ConfiguracionId",
                table: "Pacientes",
                column: "ConfiguracionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_EspecialistaIdUsuario",
                table: "Pacientes",
                column: "EspecialistaIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_TerminosycondicionesIdTerminosYCondiciones",
                table: "Pacientes",
                column: "TerminosycondicionesIdTerminosYCondiciones");

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_CuestionarioIdCuestionario",
                table: "Pregunta",
                column: "CuestionarioIdCuestionario");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionCompletada_RecomendacionIdRecomendacion",
                table: "RecomendacionCompletada",
                column: "RecomendacionIdRecomendacion");

            migrationBuilder.CreateIndex(
                name: "IX_Respuesta_PreguntaIdPregunta",
                table: "Respuesta",
                column: "PreguntaIdPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestaCarta_CartaIdCarta",
                table: "RespuestaCarta",
                column: "CartaIdCarta");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCita_CitaIdCita",
                table: "SolicitudesCita",
                column: "CitaIdCita");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCita_EspecialistaIdUsuario",
                table: "SolicitudesCita",
                column: "EspecialistaIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesPaciente_EspecialistaIdUsuario",
                table: "SolicitudesPaciente",
                column: "EspecialistaIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesPaciente_PacienteIdUsuario",
                table: "SolicitudesPaciente",
                column: "PacienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_StickersDeUsuario_PacienteIdUsuario",
                table: "StickersDeUsuario",
                column: "PacienteIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_StickersDeUsuario_StickerIdSticker",
                table: "StickersDeUsuario",
                column: "StickerIdSticker");

            migrationBuilder.CreateIndex(
                name: "IX_TestInfoModel_HistoryTestModelId",
                table: "TestInfoModel",
                column: "HistoryTestModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionWithAnswer_TestInfoModelId",
                table: "TestQuestionWithAnswer",
                column: "TestInfoModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "CuestionarioCompletados");

            migrationBuilder.DropTable(
                name: "CuestionarioPaciente");

            migrationBuilder.DropTable(
                name: "FloresDelUsuario");

            migrationBuilder.DropTable(
                name: "ImageModel");

            migrationBuilder.DropTable(
                name: "LogroPaciente");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "RecomendacionCompletada");

            migrationBuilder.DropTable(
                name: "Respuesta");

            migrationBuilder.DropTable(
                name: "RespuestaCarta");

            migrationBuilder.DropTable(
                name: "SolicitudesCita");

            migrationBuilder.DropTable(
                name: "SolicitudesPaciente");

            migrationBuilder.DropTable(
                name: "StickersDeUsuario");

            migrationBuilder.DropTable(
                name: "TestQuestionWithAnswer");

            migrationBuilder.DropTable(
                name: "Publicaciones");

            migrationBuilder.DropTable(
                name: "Flores");

            migrationBuilder.DropTable(
                name: "Logros");

            migrationBuilder.DropTable(
                name: "Emociones");

            migrationBuilder.DropTable(
                name: "Recomendaciones");

            migrationBuilder.DropTable(
                name: "Pregunta");

            migrationBuilder.DropTable(
                name: "Cartas");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Sticker");

            migrationBuilder.DropTable(
                name: "TestInfoModel");

            migrationBuilder.DropTable(
                name: "Cuestionarios");

            migrationBuilder.DropTable(
                name: "HistorialesCuestionariosCompletados");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "ConfuguracionesPaciente");

            migrationBuilder.DropTable(
                name: "Especialistas");

            migrationBuilder.DropTable(
                name: "TerminosYCondiciones");
        }
    }
}
