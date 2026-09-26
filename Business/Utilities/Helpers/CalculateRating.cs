using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Helpers
{
    public class CalculateRating
    {
        public static string CalculateGameRating(int totalCount,double positivePercentage)
        {
            if(totalCount ==0)
            {
                return "There is no comment about game";
            }
            if(totalCount < 5)
            {
                return "Comments are not enough to summarize game rating";
            }

            if (positivePercentage >= 80)
            {
                return "Overwhelmingly Positive";
            }
        
            else if (positivePercentage >= 70)
            {
                return "Mostly Positive";
            }
            else if (positivePercentage >= 40)
            {
                return "Mixed";
            }
            else if (positivePercentage >= 20)
            {
                return "Mostly Negative";
            }
            else
            {
                return "Overwhelmingly Negative";
            }
        }
    }
}
