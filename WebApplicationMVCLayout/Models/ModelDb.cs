namespace WebApplicationMVCLayout.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    public class ModelDb : DbContext
    {
        // Your context has been configured to use a 'ModelDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebApplicationMVCLayout.Models.ModelDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelDb' 
        // connection string in the application configuration file.
        public ModelDb()
            : base("name=ModelDb")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
    }

    public class Photo
    {
        public int PhotoId { get; set; }

        [DisplayName("Heading")]
        [Required]
        public string Title { get; set; }

        [DisplayName("Picture")]
        [MaxLength]
        [DataType(DataType.ImageUrl)]
        public byte[] PhotoFile { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime CreatedDate { get; set; }

        public string Username { get; set; }
    }
}