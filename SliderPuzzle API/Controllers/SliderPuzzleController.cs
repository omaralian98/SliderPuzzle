using Game;
using Game.GameLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using System;
using System.ComponentModel.DataAnnotations;

namespace SliderPuzzle_API.Controllers;
[Route("api")]
public class SliderPuzzleController(ILogger<SliderPuzzleController> _logger) : ControllerBase
{
    [HttpGet]
    [Route("solve")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [RequestTimeout(milliseconds: 180000)]
    public IActionResult Solve([Required][FromQuery] int[] board, [FromQuery] int[]? goal = null)
    {
        _logger.LogInformation("Request Started");
        if (!SliderPuzzleGame.IsValidForm(board))
        {
            _logger.LogInformation("Wasn't in correct form");
            return BadRequest("The provided board wasn't in the correct form");
        }

        int size = Convert.ToInt32(Math.Sqrt(board.Length));
        var game = new SliderPuzzleGame(size);
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
            var result = aStar.FindPath(game, SliderPuzzleGame.ManhattanDistance, HttpContext.RequestAborted);
            List<int[]> stepsIn1D = result.Result.Steps.Aggregate(new List<int[]> { }, (x, y) =>
            {
                x.Add(y.board.ConvertTo1D());
                return x;
            });
            _logger.LogInformation("Request Ended");
            return Ok(stepsIn1D);
        }
        catch
        {
            _logger.LogInformation($"The operation was canceled.");
            return StatusCode(StatusCodes.Status408RequestTimeout, "Operation timed out.");
        }
    }


    [HttpGet]
    [Route("IsSolveAble")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult IsSolveAble([Required][FromQuery] int[] board, [FromQuery] int[]? goal = null)
    {
        _logger.LogInformation("Request Started");
        if (!SliderPuzzleGame.IsValidForm(board))
        {
            return BadRequest("The provided board wasn't in correct form");
        }
        int size = Convert.ToInt32(Math.Sqrt(board.Length));
        var game = new SliderPuzzleGame(size);
        game.InitializeBoard(board.ConvertTo2D());
        if (goal is not null) game.InitializeGoal(goal.ConvertTo2D());
        _logger.LogInformation("Request Ended");
        return Ok(SliderPuzzleGame.IsSolvable(game.board, game.goal));
    }


    [HttpGet]
    [Route("Generate/{size}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult GenerateRandomBoard(int size)
    {
        _logger.LogInformation("Request Started");
        if (size <= 0)
        {
            return BadRequest("Incorrect value.");
        }
        int[,] board = SliderPuzzleGame.GenerateRandomBoard(size);
        _logger.LogInformation("Request Ended");
        return Ok(board.ConvertTo1D());
    }

    [HttpGet]
    [Route("Move/{index}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult Move([Required][FromQuery] int[] board, int index)
    {
        _logger.LogInformation("Request Started");
        if (!SliderPuzzleGame.IsValidForm(board))
        {
            return BadRequest("The provided board wasn't in correct form");
        }
        int size = Convert.ToInt32(Math.Sqrt(board.Length));
        var game = new SliderPuzzleGame(size);
        game.InitializeBoard(board.ConvertTo2D());
        var move = Coordinates._1dTo2d(index, size);
        _logger.LogInformation($"({move.x}, {move.y})");
        if (!game.MoveCell(move))
        {
            return BadRequest("This cell can't be moved.");
        }
        _logger.LogInformation("Request Ended");
        return Ok(game.board.ConvertTo1D());
    }

    [HttpGet]
    [Route("IsValidMove/{index}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult IsValidMove([Required][FromQuery] int[] board, int index)
    {
        _logger.LogInformation("Request Started");
        if (!SliderPuzzleGame.IsValidForm(board))
        {
            return BadRequest("The provided board wasn't in correct form");
        }
        int size = Convert.ToInt32(Math.Sqrt(board.Length));
        var game = new SliderPuzzleGame(size);
        game.InitializeBoard(board.ConvertTo2D());
        var move = Coordinates._1dTo2d(index, size);
        _logger.LogInformation($"({move.x}, {move.y})");
        _logger.LogInformation("Request Ended");
        return Ok(game.CanMove(move));
    }
}
