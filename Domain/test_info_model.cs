using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    // Represents the information related to a test, including the result, date, and related questions with answers.
    public class TestInfoModel
    {
        // The unique identifier for the TestInfoModel.
        // This property is marked as the primary key for the database.
        [Key]
        public int testInfoModelId { get; set; }

        // The result of the test. 
        // It is initialized to "pending" by default, meaning the result has not been determined yet.
        public string result { get; set; } = "pending";

        // The date when the test was taken.
        // The DateTime value will be set when the object is created.
        public DateTime date { get; set; }

        // A list of questions with their associated answers for the test.
        // Initialized as an empty list to avoid null reference errors.
        public List<TestQuestionWithAnswer> testQuestionWithAnswers { get; set; } = new List<TestQuestionWithAnswer>();

        // Indicates whether the test information is visible or not.
        // The default is 'true', meaning the test info is visible by default.
        public bool visible { get; set; } = true;
    }

    // Represents a goal (Logro) associated with a specific test information model.
    public class GoalWithTestInfoModel
    {
        // The TestInfoModel associated with the goal.
        // Initialized with a new instance of TestInfoModel to ensure it's never null.
        public TestInfoModel TestInfoModel { get; set; } = new TestInfoModel();

        // The goal associated with the test information. 
        // It is nullable (Goal?), meaning it might not always be assigned.
        public Goal? Logro { get; set; }
    }

    public class AchievementWithTestInfoModel
    {
        // The unique identifier for the achievement.
        public int? achievementId { get; set; }

        // The unique identifier for the test information model.
        public TestInfoModel TestInfoModel { get; set; } = new TestInfoModel();
    }


}

