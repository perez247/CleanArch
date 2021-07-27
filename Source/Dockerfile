FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy everything .dockerignore ignores the rest of the files
COPY ./Source ./

# Restore files from the Api folder which affects all other folders
WORKDIR /app/Api
RUN dotnet restore

# Build production files
RUN dotnet publish -c Release -o out
# RUN mkdir -p out

#Revert back to the default directory
WORKDIR /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app

# install System.Drawing native dependencies
# RUN apt-get update \
#     && apt-get install -y --allow-unauthenticated \
#         libc6-dev \
#         libgdiplus \
#         libx11-dev \
#      && rm -rf /var/lib/apt/lists/*

COPY --from=build-env /app/Api/out .

# Copy botdetect.xml for botdetect captcha testing
# RUN mkdir Api
COPY --from=build-env /app/Api/botdetect.xml ./

# Where to store local file data
RUN mkdir -p ../Providers/ApplicationFileProvider/FileLocalData

# Email templates for the application
RUN mkdir -p ../Providers/ApplicationEmailProvider/EmailTemplates

# Json data for seeding or prepopulating
RUN mkdir -p ../Providers/DefaultDatabaseContext/Data

# Copy file data if available
COPY --from=build-env /app/Providers/Services/ApplicationFileProvider/FileLocalData ../Providers/Services/ApplicationFileProvider/FileLocalData

# Copy email templates if available
COPY --from=build-env /Providers/Services/ApplicationEmailProvider/EmailTemplates ../Providers/Services/ApplicationEmailProvider/EmailTemplates

# Copy json data if available
COPY --from=build-env /app/Providers/DefaultDatabaseContext/Data ../Providers/DefaultDatabaseContext/Data

ENTRYPOINT ["dotnet","Api.dll"]

#  ssh root@144.126.195.222

