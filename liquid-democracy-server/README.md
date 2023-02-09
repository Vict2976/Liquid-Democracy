docker build -f liquid-democracy-server/API/Dockerfile -t iquiddemocracy:latest .

docker run --rm -d -p 5000:5000/tcp liquiddemocracy:latest


docker-compose exec server dotnet ef migrations add "Test"
