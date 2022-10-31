FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build

#pull nginx
FROM nginx:alpine
COPY --from=node /app/dist/secrets-share-frontend /usr/share/nginx/html