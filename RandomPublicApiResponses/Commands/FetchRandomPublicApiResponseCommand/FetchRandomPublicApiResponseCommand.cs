﻿using RandomPublicApiResponses.Models;

namespace RandomPublicApiResponses.Commands.FetchRandomPublicApiResponse;

public record FetchRandomPublicApiResponseCommand : IRequest<RandomApiResponseModel>;
