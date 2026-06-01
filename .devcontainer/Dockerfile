FROM mcr.microsoft.com/devcontainers/base:ubuntu-24.04

RUN apt-get update && apt-get install -y curl zip unzip && rm -rf /var/lib/apt/lists/*

ENV SDKMAN_DIR="/usr/local/sdkman"
RUN curl -s "https://get.sdkman.io" | bash

# Dynamically find the latest Java 25 version available in SDKMAN and install it
RUN bash -c "source $SDKMAN_DIR/bin/sdkman-init.sh && \
    JAVA_25_VER=\$(sdk list java | grep -E '^\s+25\.' | head -1 | awk '{print \$NF}') && \
    if [ -z \"\$JAVA_25_VER\" ]; then \
        echo 'No Java 25 version found – please check sdk list java' ; \
        exit 1 ; \
    fi && \
    echo \"Installing Java 25 version: \$JAVA_25_VER\" && \
    sdk install java \$JAVA_25_VER"

# Install Gradle
RUN bash -c "source $SDKMAN_DIR/bin/sdkman-init.sh && sdk install gradle 8.12"

RUN echo 'source "$SDKMAN_DIR/bin/sdkman-init.sh"' >> /etc/bash.bashrc
