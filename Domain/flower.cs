using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Domain
{
    // Enum to represent the different stages (steps) of a flower's development
    public enum EtapaFlor
    {
        initialFlowet,     // The initial stage of the flower
        firstStepFlower,   // The first step in the flower's growth
        secondStepFlower,  // The second step in the flower's growth
        thirdStepFlower,   // The third step in the flower's growth
        fourthStepFlower,  // The fourth step in the flower's growth
        fifthStepFlower,   // The fifth and final step in the flower's growth
    }

    // Represents a flower, with a unique identifier, name, and a collection of images
    public class Flower
    {
        // Unique identifier for the flower (primary key in the database)
        [Key]
        public int flowerId { get; set; }

        // The name of the flower
        public string name { get; set; } = "";

        // A list of images associated with the flower
        public List<ImageModel> images { get; set; } = new List<ImageModel>();
    }

    // Represents an image associated with a flower
    public class ImageModel
    {
        // Unique identifier for the image (primary key in the database)
        [Key]
        public int imageId { get; set; }

        // URL of the image
        public string url { get; set; } = "";
    }
}
