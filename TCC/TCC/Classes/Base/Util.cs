using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Classes.Base
{
    public static class Util
    {
        
        public static bool CompararMargemErro(double margemErro, double v1, double v2)
        {
            return v1 >= v2 - margemErro && v1 <= v2 + margemErro;
        }

        //Cálculo do Produto Escalar: para obter ângulos entre dois vetores (V e W)
        //Fórmula = COS-¹ (V.W / ||V||.||W|| )

        //Para calcular os vetores é preciso utilizar as posições X, Y e Z das três articulações
        //O vetor V deve ser gerado pelas articulações 1 e 2 
        //O vetor W deve ser gerado pelas articulações 2 e 3
        //Cada eixo dos vetores é calculado pela diferença entre os eixos das articulações
        //É necessário elevar o resultado ao quadrado e calcular sua raiz quadrada para obter um resulto positivo, já que a distância entre dois pontos não pode ser negativa
        private static Vector4 CriaVetor(Joint joint1, Joint joint2)
        {
            Vector4 vetor = new Vector4();

            vetor.X = Convert.ToSingle(Math.Sqrt(Math.Pow(joint1.Position.X - joint2.Position.X, 2)));
            vetor.Y = Convert.ToSingle(Math.Sqrt(Math.Pow(joint1.Position.Y - joint2.Position.Y, 2)));
            vetor.Z = Convert.ToSingle(Math.Sqrt(Math.Pow(joint1.Position.Z - joint2.Position.Z, 2)));

            return vetor;
        }

        //Produto dos Vetores: para obter o produto dos vetores soma-se o resultado do produto de cada coordenada dos vetores (V e W) utilizados 
        //Fórmula = (Vx . Wx + Vy . Wy + Vz . Wz)
        private static double CalcularProdutoVetor(Vector4 V, Vector4 W)
        {
            return V.X * W.X + V.Y * W.Y + V.Z * W.Z;
        }


        //Produto do Módulo dos Vetores: é preciso multiplicar a raiz quadrada do resultado da soma do quadrado de cada eixo do vetor (V e W)
        //           ________________      ________________
        //Fórmula: (√ Vx² + Vy² + Vz²) . (√ Wx² + Wz² + Wy²)
        private static double CalcularProdutoModulo(Vector4 V, Vector4 W)
        {
            //.Sqrt = raiz quadrada
            //.Pow = base elevada ao expoente
            return Math.Sqrt(Math.Pow(V.X, 2) + Math.Pow(V.Y, 2) + Math.Pow(V.Z, 2)) *
                   Math.Sqrt(Math.Pow(W.X, 2) + Math.Pow(W.Y, 2) + Math.Pow(W.Z, 2));
        }


        //Cosseno elevado a -1: operação chamada de arco cosseno - disponível na classe Math pelo método Acos.
        //Resultado do produto escalar é obtido em radianos e será convertido para graus: basta multiplicar por 180 e dividir pelo número PI.
        public static double CalcularProdutoEscalar(Joint joint1, Joint joint2, Joint joint3)
        {
            Vector4 vetorV = CriaVetor(joint1, joint2);
            Vector4 vetorW = CriaVetor(joint2, joint3);

            double resultado = Math.Acos(CalcularProdutoVetor(vetorV, vetorW) / CalcularProdutoModulo(vetorV, vetorW));
            double converteGraus = resultado * 180 / Math.PI;

            return converteGraus;
        }        
    }
}
