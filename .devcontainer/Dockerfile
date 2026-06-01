# Dockerfile
FROM mcr.microsoft.com/devcontainers/base:ubuntu-24.04

# Install prerequisites
RUN apt-get update && apt-get install -y curl zip unzip && rm -rf /var/lib/apt/lists/*

# Install SDKMAN!
RUN curl -s "https://get.sdkman.io" | bash
ENV SDKMAN_DIR="/usr/local/sdkman"
RUN bash -c "source $SDKMAN_DIR/bin/sdkman-init.sh && sdk version"

# Install Java 25 (adjust identifier to the exact EA build you need)
RUN bash -c "source $SDKMAN_DIR/bin/sdkman-init.sh && sdk install java 25.0.0-tem"

# Install Gradle (any version you like, e.g. 8.12)
RUN bash -c "source $SDKMAN_DIR/bin/sdkman-init.sh && sdk install gradle 8.12"

# Make sdkman commands available in all shells
RUN echo 'source "$SDKMAN_DIR/bin/sdkman-init.sh"' >> /etc/bash.bashrc