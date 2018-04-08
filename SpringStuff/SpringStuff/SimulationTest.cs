using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SimulationTest
{
    Spring[] testSprings;
    public double totalThyme;
    public bool hasFinished = false;
    public void theTest(Spring[] allSpring, double thyme, int currentCase, bool isInit)
    {
        if (isInit == false)
        {
            testSprings = allSpring;
            isInit = true;
        }
        totalThyme += thyme;
        for (int i = 1; i <= testSprings.Length; i++)
        {
            if (testSprings[i - 1].velocity < 0.005f && testSprings[i - 1].velocity > -0.005f && testSprings[i - 1].acceleration < 0.5f && testSprings[i - 1].acceleration > -0.5f && testSprings[i - 1].hasStopped == false)
            {
                Console.Write("s{0}; ePoint ({1}, {2}); dampTime {3}|", i, (int)testSprings[i - 1].endPointX, (int)testSprings[i - 1].endPointY, totalThyme);
                Console.WriteLine();
                testSprings[i - 1].hasStopped = true;
            }
        }
        /*if (testSprings[0].hasStopped && testSprings[1].hasStopped && testSprings[2].hasStopped)
        {
            hasFinished = true;
            //currentCase++;
            //testSprings = allSpring;
        }*/
    }
}
