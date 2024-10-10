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

    public class Flower
    {
        [Key]
        public int flowerId { get; set; }
        public string name { get; set; } = "";
        public List<ImageModel> images { get; set; } = new List<ImageModel>();
    }

    public class ImageModel
    {
        [Key]
        public int imageId { get; set; }
        public string url { get; set; } = "";
    }
}