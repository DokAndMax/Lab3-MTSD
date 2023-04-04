FROM golang:1.20.2-bullseye AS builder

WORKDIR /opt/application

COPY go.mod .
COPY go.sum .

RUN go mod download

COPY . .

ENV CGO_ENABLED=0

RUN GOOS=linux go build -o build/lab3golang

FROM gcr.io/distroless/static

WORKDIR /opt/application

COPY --from=builder /opt/application/templates ./templates

COPY --from=builder /opt/application/build/lab3golang .

EXPOSE 8080

CMD ["/opt/application/lab3golang", "serve"]