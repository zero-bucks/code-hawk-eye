using System;

class GitHubUrlParser
{
    public static (string Owner, string Repo) ParseRepositoryUrl(string repositoryUrl)
    {
        Uri uri = new Uri(repositoryUrl);
        string[] segments = uri.Segments;
        
        // Assuming the last two segments in the URL are the repository owner and name
        string owner = segments[segments.Length - 2].Trim('/');
        string repo = segments[segments.Length - 1].Trim('/');

        return (owner, repo);
    }
}
