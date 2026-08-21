# Build with low memory (Render Free builders are tight).
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish src/ManuelaRuiz.Web/ManuelaRuiz.Web.csproj \
    -c Release \
    -r linux-x64 \
    --self-contained false \
    -o /app/publish \
    -p:PublishReadyToRun=false \
    -p:UseSharedCompilation=false \
    -m:1

# Debian runtime (glibc) — more stable on Render Free than Alpine/musl for .NET.
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render Free ≈ 512 MB. Workstation GC + soft heap cap.
ENV ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_EnableDiagnostics=0 \
    DOTNET_GCServer=0 \
    DOTNET_GCHeapHardLimit=234881024 \
    DOTNET_ThreadPool_ForceMinWorkerThreads=1 \
    DOTNET_ThreadPool_ForceMaxWorkerThreads=4 \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    ASPNETCORE_hostBuilder__reloadConfigOnChange=false

EXPOSE 8080
ENTRYPOINT ["/bin/sh", "-c", "exec dotnet ManuelaRuiz.Web.dll --urls http://0.0.0.0:${PORT:-8080}"]
