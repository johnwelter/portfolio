package dataStructs;

public class Quaternion {

	
	public double scale;
	public Vector3D vector;	//Vector3D -> a vector of 3 values representing a point in 3D space. 
							//includes functions to normalize, scale, get magnitude,
							//and find the dot and cross products between other Vector3Ds.
	
	public Quaternion()
	{
		scale = 0.0;
		vector = new Vector3D();
	}
	public Quaternion(double s, Vector3D v)
	{
		scale = s;
		vector = v;
	}

	public String toString()
	{
		
		return "(" + scale + ", " + vector.toString() + ")";
		
	}
	
	/**
	 * Rotates quaternion around an arbitrary vector.  
	 * @param angle of rotation
	 * @param u Arbitary vector
	 * @return 
	 * RockyVector of new point location
	 */
	public Vector3D rotate(double angle, Vector3D u)
	{
		double theta = (angle*Math.PI)/180;
		
		double s = 0;
		Vector3D v = new Vector3D ();
		
		Quaternion qp = new Quaternion();
		Quaternion q = new Quaternion();
		
		Quaternion qqp = new Quaternion();
		Quaternion qin = new Quaternion();
		
		Vector3D pp = new Vector3D();
		
		//initial quaternion setup
		qp = new Quaternion(scale, vector);
				
		Vector3D uNormal = u.normalize();
				
		System.out.println("uNormal = " + uNormal.toString() + "\n");
				
		s = Math.cos(theta/2);
		v.x = Math.sin(theta/2) * uNormal.x;
		v.y = Math.sin(theta/2) * uNormal.y;
		v.z = Math.sin(theta/2) * uNormal.z;
				
		q = new Quaternion(s, v);
				
		System.out.println("initial quaternions");
		System.out.print("qp = " + qp.toString() + "\nq = " + q.toString() + "\n");
		System.out.println("");
				
		//find cross product for quaternion math
		Vector3D qqpcross = v.cross(vector);
				
		System.out.println("cross product of v and p, as well as the resulting quaternion");
		System.out.println("qqpcross = " + qqpcross.toString());
				
		//quaternion math for finding qqp
		qqp.scale= (qp.scale * q.scale) - (vector.dot(v));
		qqp.vector.x = (s*vector.x)+(qp.scale*v.x)+(qqpcross.x);		
		qqp.vector.y = (s*vector.y)+(qp.scale*v.y)+(qqpcross.y);	
		qqp.vector.z = (s*vector.z)+(qp.scale*v.z)+(qqpcross.z);
				
		System.out.println("qqp = " + qqp.toString());
		System.out.println("");
				
		qin = new Quaternion(q.scale, q.vector.scale(-1.0));
				
		//find cross product for quaternion math
		Vector3D ppcross = qqp.vector.cross(qin.vector);
				
		System.out.println("cross product of qqp and qin, as well as the resulting point");
		System.out.println("ppcross = " + ppcross.toString());
				
		//quaternion math for finding qqpqin, or the final point
		pp.x = (qqp.scale*qin.vector.x)+(qin.scale*qqp.vector.x)+(ppcross.x);
		pp.y = (qqp.scale*qin.vector.y)+(qin.scale*qqp.vector.y)+(ppcross.y);
		pp.z = (qqp.scale*qin.vector.z)+(qin.scale*qqp.vector.z)+(ppcross.z);
				
		System.out.println("pp = " + pp.toString());

		return pp;

	}
}
