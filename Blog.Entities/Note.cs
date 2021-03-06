using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities
{
    [Table("Notes")]
    public class Note : MyEntityBase
    {
        [DisplayName("Yazı Başlığı"), Required, StringLength(60)]
        public string Title { get; set; }

        [DisplayName("Yazı Metni"), Required, StringLength(2000)]
        public string Text { get; set; }

        [DisplayName("Taslak")]
        public bool IsDraft { get; set; }

        [DisplayName("Beğenilme")]
        public int LikeCount { get; set; }

        [DisplayName("Kategori")]
        public int CategoryId { get; set; }
       
        [DisplayName("Fotoğraf Uzantısı")]
        public string ImagePath { get; set; }

        public virtual BlogUser Owner { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }


        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}
