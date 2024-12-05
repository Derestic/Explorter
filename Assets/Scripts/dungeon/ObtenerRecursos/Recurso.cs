using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recurso : MonoBehaviour
{

    enum recurso { Madera, Hierro, PiedraMagica };
    [SerializeField]
    recurso recursoObtenible;
    private int recolectSpeed;
    private int quantity;

    public void Awake() {
        recolectSpeed = Random.Range(1,3);
        quantity = Random.Range(4, 7); 
    }
    public string getRecurso() {
        if (recursoObtenible == recurso.Madera) return "Madera";
        if (recursoObtenible == recurso.Hierro) return "Hierro";
        if (recursoObtenible == recurso.PiedraMagica) return "Piedra Magica";
        return "";
    }
    public int getQuantity() {
        quantity -= recolectSpeed;
        if (quantity <= 0) {
            Destroy(gameObject);
            return quantity + recolectSpeed;
        }
        return recolectSpeed;
    }
}
