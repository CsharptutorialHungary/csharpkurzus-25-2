using System.Text;

namespace Calculator.HTTP;

//Example to parse:
//POST /cgi-bin/process.cgi HTTP/1.1
//Host: www.example.com
//User-Agent: Mozilla/5.0 (compatible; MSIE5.01; Windows NT)
//Content-Type: application/x-www-form-urlencoded
//Content-Length: 27
//Accept-Language: en-us
//Accept-Encoding: gzip, deflate
//Connection: Keep-Alive

//licenseID=string&content=string
//
//

public static class HttpRequestParser
{
    private enum ParserState
    {
        FirstLine,
        Headers,
        Body,
    }

    public static async ValueTask<HttpRequest> ParseAsync(Stream stream, int port)
    {
        using var reader = new StreamReader(stream, leaveOpen: true);
        StringBuilder body = new();
        string? line;
        ParserState state = ParserState.FirstLine;

        var resultRequest = new HttpRequest();

        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (state == ParserState.FirstLine)
            {
                var parts = line.Split(' ', 3, StringSplitOptions.TrimEntries);
                if (parts.Length < 3)
                    throw new InvalidOperationException("Invalid request line format.");

                resultRequest.Method = Enum.Parse<Method>(parts[0], ignoreCase: true);
                resultRequest.Path = new Uri($"http://localhost:{port}{parts[1]}", UriKind.Absolute);
                resultRequest.Version = parts[2];
                state = ParserState.Headers;
            }
            else if (state == ParserState.Headers)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (resultRequest.Method == Method.Get)
                    {
                        break; // No body for GET requests
                    }
                    // Empty line indicates the end of headers
                    state = ParserState.Body;
                    continue;
                }

                var headerParts = line.Split(':', 2, StringSplitOptions.TrimEntries);

                if (headerParts.Length != 2)
                    throw new InvalidOperationException("Invalid header format.");

                resultRequest.Headers[headerParts[0]] = headerParts[1];
            }
            else if (state == ParserState.Body)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    break; // End of body
                }
                body.AppendLine(line);
            }
        }

        if (body.Length > 0)
        {
            resultRequest.Body = body.ToString().TrimEnd();
        }


        return resultRequest;
    }
}