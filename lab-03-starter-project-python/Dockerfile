FROM python:3.10-buster

WORKDIR /opt/application

COPY . .

RUN pip install -r ./requirements/backend.in

EXPOSE 8080

ENTRYPOINT ["uvicorn", "spaceship.main:app", "--host=0.0.0.0", "--port=8080"]