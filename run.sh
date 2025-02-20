#!/bin/sh
docker build --no-cache -t api . && docker run -p 5000:5000 api
