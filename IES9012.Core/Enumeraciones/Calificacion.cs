namespace IES9012.Core.Enumeraciones
{
    public enum Calificacion
    {
        //vamos a crear un enum
        //un enum, es algo que asocia un texto a un valor numerico
        //es una numeracion
        //vamos a crear una tabla de notas nuevas

        //De forma predeterminada,los vlaores de constante asociados a miembros de enumeracion
        //son del tipo entero(int)
        //comienzan con cero y aumentan en uno despues del orden del texto de la definicion
        //puedo especificar explicitamente cualquier otro tipo de numero entero como un tipo
        //subyacente de un tipo de numeracion
        //tambien puede especificar explicitamente los valores de constante asociados, como
        //se muestra en el ejemplo siguiente
        Mal = 0,
        Deficiente = 20,
        Regular = 40,
        Bien = 60,
        MuyBien = 80,
        Sobresaliente = 100,
        //una enumeracion me crea datos asociados de valor de tipo entero
    }
}