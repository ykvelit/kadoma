using Ykvelit.Core.Application.Commands;

namespace Application.Commands.ShortenUrl;
public record ShortenUrlCommand(string Url) : ICommand<string>;
