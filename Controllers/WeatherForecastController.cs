using Microsoft.AspNetCore.Mvc;

namespace matthew_prac.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    Dictionary<char, int> alphaWheel = new Dictionary<char, int>(){
        {'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6},
        {'G', 7}, {'H', 8}, {'H', 9}, {'J', 10}, {'K', 11}, {'L', 12},
        {'M', 13}, {'N', 14}, {'O', 15}, {'P', 16}, {'Q', 17}, {'R', 18},
        {'S', 19}, {'T', 20}, {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24},
        {'Y', 25}, {'Z', 26}
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("inputString")]
    public double Get(string inputString)
    {
        inputString = inputString.ToUpper();

        int forwardDistance = 0;
        int backwardDistance = 0;

        int targetPos = 0;
        int currentPos = 0;
        char currentLetter = 'A';

        double time = 0.0;

        foreach(char c in inputString)
        {
            targetPos = alphaWheel[c];

            if(c == currentLetter)
            {
                time += 2.5;
            }
            else
            {
                forwardDistance = Math.Abs(targetPos - currentPos);
                backwardDistance = Math.Abs(0 - currentPos) + Math.Abs(26 - targetPos);

                time += (forwardDistance < backwardDistance) ? forwardDistance * 5 : backwardDistance * 5;

                forwardDistance = 0;
                backwardDistance = 0;

                currentLetter = c;
                currentPos = targetPos;
            }
        }

        return time;
    }
}
