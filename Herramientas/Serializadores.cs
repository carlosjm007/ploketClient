[System.Serializable]
public class identificacion
{
	public string id;
	public string sala_id;
	public string nombre;
}

[System.Serializable]
public class ubicacion
{
	public string id;
	public float angulo;
	public float magnitud;
	public float reloj;
	public bool disparo;
	public float reloj_disparo;
}

[System.Serializable]
public class server
{
	public string id;
	public int cuenta;
}

public static class info {
    public static string id { get; set; }
    public static string sala { get; set; }
    public static string nickname { get; set; }
}