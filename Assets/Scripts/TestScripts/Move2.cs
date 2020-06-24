using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
	public GameObject wheelCollider; 

	public float wheelRadius = 0.15f; //рад колёс
	public float suspensionOffset = 0.05f; //Смещение колеса относительно начальной позиции, когда оно не касается поверхности.

	public float trackTextureSpeed = 2.5f; 

	public GameObject leftTrack;  
	public Transform[] leftTrackUpperWheels; //не добавляются в wheelCollider
	public Transform[] leftTrackWheels; // добавляются в wheelCollider
	public Transform[] leftTrackBones; //кости

	public GameObject rightTrack; 
	public Transform[] rightTrackUpperWheels; //не добавляются в wheelCollider
	public Transform[] rightTrackWheels; // добавляются в wheelCollider
	public Transform[] rightTrackBones; //кости



	protected WheelData[] leftTrackWheelData; //массивы о лев и прав колесе
	protected WheelData[] rightTrackWheelData; //

	protected float leftTrackTextureOffset = 0.0f; //смещение тесктур
	protected float rightTrackTextureOffset = 0.0f;

	public float rotateOnStandTorque = 1500.0f; //крутящий момент
	public float rotateOnStandBrakeTorque = 500.0f; //тормозной момент
	public float maxBrakeTorque = 1000.0f; //макс тормозной момент


	public float forwardTorque = 500.0f; //Крутящий момент при движении (вперед, назад)
	public float rotateOnMoveBrakeTorque = 400.0f; //Tормозной момент при повороте во время движения
	public float minBrakeTorque = 0.0f; //Минимальный тормозной момент
	public float minOnStayStiffness = 0.06f; //Минимальное боковое трение при повороте на месте.
	public float minOnMoveStiffness = 0.05f;  //Минимальное боковое трение при повороте во время движения 
	public float rotateOnMoveMultiply = 2.0f; //Множитель крутящего момента при повороте во время движения
	public class WheelData
	{
		public Transform wheelTransform;
		public Transform boneTransform;
		public WheelCollider col;
		public Vector3 wheelStartPos;
		public Vector3 boneStartPos;
		public float rotation = 0.0f;
		public Quaternion startWheelAngle;  //начальные углы поворота колеса

		
	}
	void Awake()
	{

		leftTrackWheelData = new WheelData[leftTrackWheels.Length]; //обьявляем размерность массивов
		rightTrackWheelData = new WheelData[rightTrackWheels.Length]; 

		for (int i = 0; i < leftTrackWheels.Length; i++)
		{
			leftTrackWheelData[i] = SetupWheels(leftTrackWheels[i], leftTrackBones[i]);  //2 
		}

		for (int i = 0; i < rightTrackWheels.Length; i++)
		{
			rightTrackWheelData[i] = SetupWheels(rightTrackWheels[i], rightTrackBones[i]); //2  
		}


		// танк проваливается под пол, решение данной проблемы:
		Vector3 offset = transform.position;
		offset.z += 0.01f;
		transform.position = offset;

	}

	
	 WheelData SetupWheels(Transform wheel, Transform bone)
	{  
		WheelData result = new WheelData();

		GameObject go = new GameObject("Collider_" + wheel.name); 
		go.transform.parent = transform; 
		go.transform.position = wheel.position; 
		go.transform.localRotation = Quaternion.Euler(0, wheel.localRotation.y, 0); //7 

		WheelCollider col = (WheelCollider)go.AddComponent(typeof(WheelCollider));//8 
		WheelCollider colPref = wheelCollider.GetComponent<WheelCollider>();//9 


		// присвоение к новому обьекту парамтры префаба танка
		col.mass = colPref.mass;
		col.center = colPref.center;
		col.radius = colPref.radius;
		col.suspensionDistance = colPref.suspensionDistance;
		col.suspensionSpring = colPref.suspensionSpring;
		col.forwardFriction = colPref.forwardFriction;
 
		result.wheelTransform = wheel; 
		result.boneTransform = bone; 
		result.col = col; 
		result.wheelStartPos = wheel.transform.localPosition; 
		result.boneStartPos = bone.transform.localPosition; 
		result.startWheelAngle = wheel.transform.localRotation; 

		return result; 
	}
	void FixedUpdate()
	{

		
		float accelerate = 0;
		float steer = 0;

		accelerate = Input.GetAxis("Vertical");  
		steer = Input.GetAxis("Horizontal"); 

		UpdateWheels(accelerate, steer); 

	}

	public void UpdateWheels(float accel, float steer)
	{ 
		float delta = Time.fixedDeltaTime;  

		float trackRpm = CalculateSmoothRpm(leftTrackWheelData); //средняя скорость вращения "колес"
		foreach (WheelData w in leftTrackWheelData)
		{  
			w.wheelTransform.localPosition = CalculateWheelPosition(w.wheelTransform, w.col, w.wheelStartPos); //локал позиция колеса по Y 
			w.boneTransform.localPosition = CalculateWheelPosition(w.boneTransform, w.col, w.boneStartPos); //локал позиция кости по Y    

			w.rotation = Mathf.Repeat(w.rotation + delta * trackRpm * 360.0f / 60.0f, 360.0f);  //угол вращения колеса
			w.wheelTransform.localRotation = Quaternion.Euler(w.rotation, w.startWheelAngle.y, w.startWheelAngle.z);
			CalculateMotorForce(w.col, accel, steer);
		}


		leftTrackTextureOffset = Mathf.Repeat(leftTrackTextureOffset + delta * trackRpm * trackTextureSpeed / 60.0f, 1.0f); //смещение текстуры гусеницы
		//GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, -leftTrackTextureOffset)); 

		trackRpm = CalculateSmoothRpm(rightTrackWheelData);  

		foreach (WheelData w in rightTrackWheelData)
		{  //4   
			w.wheelTransform.localPosition = CalculateWheelPosition(w.wheelTransform, w.col, w.wheelStartPos); 
			w.boneTransform.localPosition = CalculateWheelPosition(w.boneTransform, w.col, w.boneStartPos); 

			w.rotation = Mathf.Repeat(w.rotation + delta * trackRpm * 360.0f / 60.0f, 360.0f); 
			w.wheelTransform.localRotation = Quaternion.Euler(w.rotation, w.startWheelAngle.y, w.startWheelAngle.z);
			CalculateMotorForce(w.col, accel, -steer);

		}

		rightTrackTextureOffset = Mathf.Repeat(rightTrackTextureOffset + delta * trackRpm * trackTextureSpeed / 60.0f, 1.0f);  
		//GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, -rightTrackTextureOffset));   



		// вращение верхних колес
		for (int i = 0; i < leftTrackUpperWheels.Length; i++)
		{   
			leftTrackUpperWheels[i].localRotation = Quaternion.Euler(leftTrackWheelData[0].rotation, leftTrackWheelData[0].startWheelAngle.y, leftTrackWheelData[0].startWheelAngle.z);  //11 
		}

		for (int i = 0; i < rightTrackUpperWheels.Length; i++)
		{   
			rightTrackUpperWheels[i].localRotation = Quaternion.Euler(rightTrackWheelData[0].rotation, rightTrackWheelData[0].startWheelAngle.y, rightTrackWheelData[0].startWheelAngle.z);  //11 
		}
	}
	private float CalculateSmoothRpm(WheelData[] w)
	{ 
		float rpm = 0.0f;

		List<int> grWheelsInd = new List<int>(); 

		for (int i = 0; i < w.Length; i++)
		{  
			//проверка на нахождение индексов ктр касаются земли
			if (w[i].col.isGrounded)
			{  
				grWheelsInd.Add(i); //14 
			}
		}
		// нахождение ср скорости
		if (grWheelsInd.Count == 0)
		{     
			foreach (WheelData wd in w)
			{  
				rpm += wd.col.rpm;  				 
			}

			rpm /= w.Length; 

		}
		else
		{   

			for (int i = 0; i < grWheelsInd.Count; i++)
			{  
				rpm += w[grWheelsInd[i]].col.rpm; 
			}

			rpm /= grWheelsInd.Count; 
		}

		return rpm; 
	}
	private Vector3 CalculateWheelPosition(Transform w, WheelCollider col, Vector3 startPos)
	{  
		WheelHit hit;

		Vector3 lp = w.localPosition;
		if (col.GetGroundHit(out hit))
		{
			lp.y -= Vector3.Dot(w.position - hit.point, transform.up) - wheelRadius;

		}
		else
		{
			lp.y = startPos.y - suspensionOffset;

		}

		return lp;
	}

	public void CalculateMotorForce(WheelCollider col, float accel, float steer)
	{
		WheelFrictionCurve fc = col.sidewaysFriction;
		//крутящий момент
		if (accel == 0 && steer == 0)// если не нажата ни одна клавиша
		{
			col.brakeTorque = maxBrakeTorque;
		}
		else if (accel == 0.0f) // если не нажата клавиша вперед
		{
			col.brakeTorque = rotateOnStandBrakeTorque; //тормозной момент
			col.motorTorque = steer * rotateOnStandTorque;
			fc.stiffness = 1.0f + minOnStayStiffness - Mathf.Abs(steer);
		}
		else
		{
			col.brakeTorque = minBrakeTorque;  
			col.motorTorque = accel * forwardTorque;  

			if (steer < 0)
			{  
				col.brakeTorque = rotateOnMoveBrakeTorque; //при повороте вправо , лев колайдеры будут притормаживать
				col.motorTorque = steer * forwardTorque * rotateOnMoveMultiply;//при повороте лев колайдеры будут поворачивать в др сторону 
				fc.stiffness = 1.0f + minOnMoveStiffness - Mathf.Abs(steer);  
			}
			if (steer > 0)
			{ 

				col.motorTorque = steer * forwardTorque * rotateOnMoveMultiply;
				fc.stiffness = 1.0f + minOnMoveStiffness - Mathf.Abs(steer); 
			}

		}
		if (fc.stiffness > 1.0f) fc.stiffness = 1.0f; 		 
		col.sidewaysFriction = fc; 

		if (col.rpm > 0 && accel < 0)
		{ // если едем вперед и нажмимаем назад то будет произведен тормозной момент 
			col.brakeTorque = maxBrakeTorque;  
		}
		else if (col.rpm < 0 && accel > 0)
		{ 
			col.brakeTorque = maxBrakeTorque; 
		}

	}
}

