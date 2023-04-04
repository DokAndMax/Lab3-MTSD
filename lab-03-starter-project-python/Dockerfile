FROM python:3.10-buster

WORKDIR /opt/application

COPY ./requirements/requirements.txt ./requirements/

RUN pip install -r ./requirements/requirements.txt

COPY ./build ./build

COPY ./spaceship ./spaceship

EXPOSE 8080

ENTRYPOINT ["uvicorn", "spaceship.main:app", "--host=0.0.0.0", "--port=8080"]