 using Business.Contracts;
using Data.contracts;
using Data.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _badgeRepository;

        public BadgeService(IBadgeRepository badgeRepository)
        {
            _badgeRepository = badgeRepository;
        }

        public int AddBadge(Badge badge)
        {
            if (badge == null) return 0;
            return _badgeRepository.AddBadge(badge);
        }

        public Badge? GetBadge(int badgeId)
        {
            if (badgeId <= 0) return null;
            return _badgeRepository.GetBadge(badgeId);
        }

        public List<Badge>? GetAllBadges()
        {
            return _badgeRepository.GetBadges();
        }

        public bool UpdateBadge(Badge badge)
        {
            if (badge == null) return false;
            return _badgeRepository.UpdateBadge(badge);
        }

        public bool DeleteBadge(int badgeId)
        {
            if (badgeId <= 0) return false;
            return _badgeRepository.DeleteBadge(badgeId);
        }
    }
}