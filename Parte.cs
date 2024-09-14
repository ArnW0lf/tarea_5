using System.Collections.Generic;
using OpenTK;

namespace Tarea3Grafica
{
    public class Parte
    {
        private List<Poligono> poligonos = new List<Poligono>();
        private List<int> ids = new List<int>();
        public CentroDeMasa CentroDeMasa { get; private set; }

        public void Add(int id, Poligono poligono)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                poligonos[index] = poligono;
            }
            else
            {
                ids.Add(id);
                poligonos.Add(poligono);
            }
            CalcularCentroDeMasa();
        }

        public Poligono Get(int id)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                return poligonos[index];
            }
            return null;
        }

        public void Delete(int id)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                ids.RemoveAt(index);
                poligonos.RemoveAt(index);
            }
            CalcularCentroDeMasa();
        }

        public void Draw()
        {
            foreach (var poligono in poligonos)
            {
                poligono.Draw();
            }
        }

        // Método para calcular el centro de masa de la parte
        private void CalcularCentroDeMasa()
        {
            float sumaX = 0, sumaY = 0, sumaZ = 0;
            int totalVertices = 0;

            foreach (var poligono in poligonos)
            {
                foreach (var vertice in poligono.GetVertices())
                {
                    sumaX += vertice.X;
                    sumaY += vertice.Y;
                    sumaZ += vertice.Z;
                    totalVertices++;
                }
            }

            if (totalVertices > 0)
            {
                CentroDeMasa = new CentroDeMasa(sumaX / totalVertices, sumaY / totalVertices, sumaZ / totalVertices);
            }
        }

        // Aplicar una traslación a toda la parte
        public void TrasladarParte(Vector3 desplazamiento)
        {
            foreach (var poligono in poligonos)
            {
                poligono.AplicarTransformacion(v => Transformacion.Trasladar(v, desplazamiento));
            }
            CalcularCentroDeMasa();
        }

        // Aplicar una rotación a toda la parte
        public void RotarParte(float angulo, Vector3 eje)
        {
            Vector3 centro = new Vector3(CentroDeMasa.X, CentroDeMasa.Y, CentroDeMasa.Z);
            foreach (var poligono in poligonos)
            {
                poligono.AplicarTransformacion(v => Transformacion.Rotar(v, centro, angulo, eje));
            }
            CalcularCentroDeMasa();
        }

        // Aplicar un escalado a toda la parte
        public void EscalarParte(Vector3 factorEscalado)
        {
            Vector3 centro = new Vector3(CentroDeMasa.X, CentroDeMasa.Y, CentroDeMasa.Z);
            foreach (var poligono in poligonos)
            {
                poligono.AplicarTransformacion(v => Transformacion.Escalar(v, centro, factorEscalado));
            }
            CalcularCentroDeMasa();
        }
    }

}