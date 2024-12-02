using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Inventario
{
    Dictionary<string, int> recursos = new Dictionary<string, int>();
    string[] nombreRecursos = { "Madera", "Hierro", "Piedra magica" };
    static Inventario instance;

    private Inventario() {
        if (instance == null)
        {
            for (int i = 0; i < nombreRecursos.Length; i++)
            {
                recursos.Add(nombreRecursos[i], 0);
            }
        }
    }
    public static Inventario Instance()
    {
        if(instance == null) return new Inventario();
        else return instance;
    }

    public string[] getKeyRecursos()
    {
        return nombreRecursos;
    }
    public int getRecurso(string recurso)
    {
        return recursos[recurso];
    }
    public void setRecurso(string recurso, int v)
    {
        recursos[recurso] = v;
    }
    public void addRecurso(string recurso, int v)
    {
        recursos[recurso] += v;
    }
}
