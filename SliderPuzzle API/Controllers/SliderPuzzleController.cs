using Game;
using Game.GameLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using System;

namespace SliderPuzzle_API.Controllers;
[Route("api/[Controller]")]
public class SliderPuzzleController(ILogger<SliderPuzzleController> _logger) : ControllerBase
{
    [HttpGet]
    [Route("solve")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public IActionResult Solve([FromQuery] int[] board, [FromQuery] int[]? goal = null)
    {
        // Set the cancellation token timeout to 2 minutes
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        var token = cts.Token;
        if (!SliderPuzzleGame.IsValidForm(board))
        {
            _logger.LogInformation("\nWasn't in correct form");
            return BadRequest("The provided board wasn't in the correct form");
        }

        var game = new SliderPuzzleGame(Convert.ToInt32(Math.Sqrt(board.Length)));
        var aStar = new AStarSearch<SliderPuzzleGame>();
        game.InitializeBoard(board.ConvertTo2D());

        if (goal is not null) game.InitializeGoal(goal.ConvertTo2D());

        if (!SliderPuzzleGame.IsSolvable(game.board, game.goal))
        {
            _logger.LogInformation(game.ToString() + " Can't be solved");
            _logger.LogInformation(game.GetGoalBoard());
            return NotFound("The provided board can't be solved");
        }
        try
        {
            var steps = new List<SliderPuzzleGame>();

            Task.Run(() =>
            {
                (var step, var discoveredNodes, var visitedNodes) =
                    aStar.FindPath(game, SliderPuzzleGame.ManhattanDistance);
                steps = step;
            }, token).Wait(); // Wait for the task to complete or throw an exception if cancelled

            List<int[]> stepsIn1D = steps.Aggregate(new List<int[]> { }, (x, y) =>
            {
                x.Add(y.board.ConvertTo1D());
                return x;
            });

            return Ok(stepsIn1D);
        }
        catch (OperationCanceledException)
        {
            // Handle cancellation here
            _logger.LogInformation("The operation was canceled after 2 minutes.");
            return StatusCode(StatusCodes.Status408RequestTimeout, "Operation timed out");
        }
    }


    [HttpGet]
    [Route("IsSolveAble")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult IsSolveAble([FromQuery] int[] board, [FromQuery] int[]? goal = null)
    {
        _logger.LogInformation("Request Started");
        if (!SliderPuzzleGame.IsValidForm(board))
        {
            return BadRequest("The provided board wasn't in correct form");
        }
        var game = new SliderPuzzleGame(Convert.ToInt32(Math.Sqrt(board.Length)));
        game.InitializeBoard(board.ConvertTo2D());
        if (goal is not null) game.InitializeGoal(goal.ConvertTo2D());
        _logger.LogInformation("Request Ended");
        return Ok(SliderPuzzleGame.IsSolvable(game.board, game.goal));
    }

    [HttpGet]
    [Route("test/{str}")]
    [RequestTimeout(milliseconds: 60)]
    public async Task<IActionResult> Test(string str)
    {
        var ts = new CancellationTokenSource();
        CancellationToken ct = ts.Token;
        Task.Factory.StartNew(() =>
        {
            while (true)
            {
                // do some heavy work here
                Thread.Sleep(100);
                if (ct.IsCancellationRequested)
                {
                    // another thread decided to cancel
                    Console.WriteLine("task canceled");
                    break;
                }
            }
        }, ct);

        // Simulate waiting 3s for the task to complete
        Thread.Sleep(3000);

        // Can't wait anymore => cancel this task 
        ts.Cancel();
        _logger.LogInformation("Started");
        try
        {
            await Task.Delay(2000, HttpContext.RequestAborted);

        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
        _logger.LogInformation("Finished");
        return Ok(str);
    }

}
