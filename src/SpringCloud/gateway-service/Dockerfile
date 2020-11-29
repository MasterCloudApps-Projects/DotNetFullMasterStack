FROM maven:3.6.3-jdk-11-slim as build

WORKDIR /src
COPY pom.xml .
COPY src/ src/
RUN mvn package -DskipTests

FROM build as unittest
WORKDIR /src

#run image
FROM openjdk:11-jre
EXPOSE 8762

WORKDIR /app
COPY --from=build ./src/target/*.jar app.jar
# CMD java -jar *.jar
ENTRYPOINT ["java", "-jar", "/app/app.jar"]