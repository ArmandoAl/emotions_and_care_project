using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.contracts;
using Data.Implementations;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.implementations
{
    public class GoalRepository : IGoalRepository
    {
        public int AddGoal(Goal goal)
        {
            if(goal == null) return 0;
            
             var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.goals.Add(goal);
                db.SaveChanges();
                return goal.goalId;
            }
        }

        public int AddGoalPatient(int idPaciente, int id)
        {
            if(idPaciente == 0 || id == 0) return 0;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;

            using (var db = new DBContext(options: connectionOptions))
            {
                Patient? paciente = db.patients.Where(x => x.userId == idPaciente).Include(x => x.goals).FirstOrDefault();

                var goal = db.goals.Find(id);

                if(paciente == null || goal == null) return 0;

                paciente.goals.Add(goal);
                db.SaveChanges();
                return goal.goalId;
            }
        }

        public bool DeleteGoal(int id)
        {
            if(id == 0) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                var goal = db.goals.Find(id);
                db.goals.Remove(goal!);
                db.SaveChanges();
                return true;
            }
        }

        public List<Goal> GetAllGoals()
        {
            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.goals.ToList();
            }
        }

        public Goal GetGoal(int id)
        {
            if(id == 0) return null!;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.goals.Find(id)!;
            }
        }

        public Goal GetGoal(string nombre)
        {
            if(string.IsNullOrEmpty(nombre)) return null!;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                return db.goals.Where(l => l.name == nombre).FirstOrDefault()!;
            }
        }

        public bool UpdateGoal(Goal goal)
        {
            if(goal == null) return false;

            var connectionOptions = new DbContextOptionsBuilder<DBContext>()
              .UseSqlServer(Data.Helpers.Constants.ConnectionString)
              .Options;
            using (var db = new DBContext(options: connectionOptions))
            {
                db.goals.Update(goal);
                db.SaveChanges();
                return true;
            }
        }
    }
}