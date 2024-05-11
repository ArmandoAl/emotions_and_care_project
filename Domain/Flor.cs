using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{

    public enum EtapaFlor
    {
      initialFlowet,
      firstStepFlower,
      secondStepFlower,
      thirdStepFlower,
      fourthStepFlower,
      fifthStepFlower,
    }

    public class Flor
    {
        [Key]
        public int IdFlor { get; set; }
        public string Nombre { get; set; } = "";
        public List<ImageModel> Imagenes { get; set; } = new List<ImageModel>();
    }

    public class ImageModel
    {
        [Key]
        public int IdImage { get; set; }
        public string Url { get; set; } = "";
    }
}