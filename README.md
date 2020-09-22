# aks_autoscaller_demo

```
docker build AKS.Autoscaller -t psbds/aksautoscaller:v2
docker push psbds/aksautoscaller:v2


kubectl apply -f deployment/deployment.yaml
kubectl apply -f deployment/service.yaml
kubectl apply -f deployment/hpa.yaml
```