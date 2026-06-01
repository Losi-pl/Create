FROM mcr.microsoft.com/devcontainers/base:ubuntu-24.04

RUN apt-get update && apt-get install -y curl zip unzip && rm -rf /var/lib/apt/lists/*

# Tell SDKMAN where to install *before* running the installer
ENV SDKMAN_DIR="/usr/local/sdkman"
RUN curl -s "https://get.sdkman.io" | bash

# Install Java 25 (use `sdk list java` to find the exact identifier)
RUN bash -c "source $SDKMAN_DIR/bin/sdkman-init.sh && sdk install java 25.0.0-tem"

# Install Gradle
RUN bash -c "source $SDKMAN_DIR/bin/sdkman-init.sh && sdk install gradle 8.12"

# Make sdkman available in interactive shells
RUN echo 'source "$SDKMAN_DIR/bin/sdkman-init.sh"' >> /etc/bash.bashrc
