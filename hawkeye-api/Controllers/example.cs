// ExampleController.cs

using Microsoft.AspNetCore.Mvc;
using Octokit;
using System;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetGitHubSourceCode()
    {
        // Hardcoded GitHub repository URL
        string githubRepoUrl = "https://github.com/mohammed-saalim/abandoned-app";

        // Parse GitHub URL to get repository owner and name
        (string owner, string repo) = GitHubUrlParser.ParseRepositoryUrl(githubRepoUrl);

        // Continue with the GitHub API code
        if (await RetrieveSourceCode(owner, repo))
        {
            return Ok("Source code retrieved successfully!");
        }
        else
        {
            return StatusCode(500, "Failed to retrieve source code.");
        }
    }

      private async Task<bool> RetrieveSourceCode(string repositoryOwner, string repositoryName)
{
    {
        var github = new GitHubClient(new ProductHeaderValue("MyGitHubApiClient"));

        // // Authenticate with your GitHub credentials (replace with your username and token)
        // var credentials = new Credentials("mohammed-saalim", "github token");
        // github.Credentials = credentials;

        try
        {
            // Get the contents of a directory in the specified branch
            var contents = await github.Repository.Content.GetAllContentsByRef(repositoryOwner, repositoryName, "/", "main");

            // Download and print the content of each file
            foreach (var file in contents)
            {
                if (file.Type == ContentType.File)
                {
                    var fileContentBytes = await github.Repository.Content.GetRawContent(repositoryOwner, repositoryName, file.Path);
                    var fileContent = Encoding.UTF8.GetString(fileContentBytes);
                    Console.WriteLine($"Content of {file.Name}:\n{fileContent}\n");
                }
            }

            return true; // Success
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false; // Failure
        }
        }
    }
}

