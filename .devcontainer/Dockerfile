FROM ghcr.io/bitsofabyte/docker-dalamud:staging

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

RUN adduser --disabled-password --gecos "" dev

RUN apk add --update openssh