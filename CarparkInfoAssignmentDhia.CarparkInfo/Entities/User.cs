using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Entities;
public class User
{
    public int Id { get; set; }
    public virtual ICollection<UserFavoriteCarpark> UserFavoriteCarparks { get; set; } = new HashSet<UserFavoriteCarpark>();
}
