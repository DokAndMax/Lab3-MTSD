FROM golang:1.20.2-alpine

WORKDIR /opt/application

COPY go.mod .
COPY go.sum .

RUN go mod download

COPY . .

RUN go build -o build/lab3golang

EXPOSE 8080

CMD ["./build/lab3golang", "serve"]