using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstUnitOfWork.Models
{
    [Table("Account", Schema = "Bank")]
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public double Balance { get; set; }
        //public virtual ICollection<User> Users {get; set; }

        public Account()
        {
            //Users = new HashSet<User>();
        }
    }
}
