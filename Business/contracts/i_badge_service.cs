using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    
    public interface IBadgeService
    {
        int AddBadge(Badge badge);
        Badge? GetBadge(int badgeId);
        List<Badge>? GetAllBadges();
        bool UpdateBadge(Badge badge);
        bool DeleteBadge(int badgeId);
    }
}