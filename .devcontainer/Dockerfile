FROM mcr.microsoft.com/devcontainers/base:ubuntu-24.04

# Install prerequisites
RUN apt-get update && apt-get install -y curl tar unzip zip && rm -rf /var/lib/apt/lists/*

# Download and install the latest Java 25 early‑access build from Adoptium
RUN curl -sL "https://api.adoptium.net/v3/binary/latest/25/ea/linux/x64/jdk/hotspot/normal/eclipse?project=jdk" \
    | tar xz -C /opt

# Give the extracted folder a fixed name (the actual folder name contains a version)
RUN mv /opt/jdk-25* /opt/jdk-25

# Install Gradle via SDKMAN (it handles Gradle perfectly fine)
ENV SDKMAN_DIR="/usr/local/sdkman"
RUN curl -s "https://get.sdkman.io" | bash
RUN bash -c "source $SDKMAN_DIR/bin/sdkman-init.sh && sdk install gradle 8.12"
RUN echo 'source "$SDKMAN_DIR/bin/sdkman-init.sh"' >> /etc/bash.bashrc
