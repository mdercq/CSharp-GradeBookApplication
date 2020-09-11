using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            this.Type = GradeBookType.Ranked;
        }

        /// <summary>
        /// Ranked-grading grades students not based on their individual performance, but rather their performance compared to their peers. This means the 20% of the students with the highest grade of a class get an A, the next highest 20% get a B, etc. There are many ways to calculate this. I'd recommend figuring out how many students it takes to drop a letter grade (20% of the total number of students) X, put all the average grades in order, then figure out where the input grade would fit in the series of grades. Every X students with higher grades than the input grade knocks the letter grade down until you reach F.
        /// </summary>
        /// <param name="averageGrade"></param>
        /// <returns>char: Grade as letter [A->F]</returns>
        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5) throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            
            int twentyPercent = Students.Count / 5;
            int studentsBetterThanThis = Students.Where(x => x.AverageGrade > averageGrade).Count();
            if (studentsBetterThanThis < twentyPercent) return 'A';
            else if (studentsBetterThanThis < twentyPercent * 2) return 'B';
            else if (studentsBetterThanThis < twentyPercent * 3) return 'C';
            else if (studentsBetterThanThis < twentyPercent * 4) return 'D';

            return 'F';
        }
        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
