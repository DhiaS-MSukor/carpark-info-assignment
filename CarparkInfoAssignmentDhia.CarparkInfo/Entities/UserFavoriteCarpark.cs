using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarparkInfoAssignmentDhia.CarparkInfo.Entities;
public class UserFavoriteCarpark
{
    public int User_Id { get; set; }
    public int Carpark_Id { get; set; }
    public virtual User? User { get; set; }
    public virtual Carpark? Carpark { get; set; }
}
