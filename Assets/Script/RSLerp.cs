using UnityEngine;

public static class RSLerp {
	//http://gizma.com/easing/
	//http://www.dzone.com/snippets/robert-penner-easing-equations
	
	
	///////////// SIMPLE TWEEN ////////////////////////////
	// t: current time, a: start value, b: end value, c = change value, t: elapsed time, d: duration (optional)
	public static float LinearTween (float a, float b, float t, float d = 1f) {
	// simple linear tweening - no easing
		float c = b-a; 
		t = Mathf.Max(0,Mathf.Min(d, t));
		return c*t/d + a;
		
	}
	public static Vector3 LinearTween (Vector3 a, Vector3 b, float t, float d = 1f) {
	// simple linear tweening - no easing
		Vector3 c = b-a;
		t = Mathf.Max(0,Mathf.Min(d, t));
		return c*t/d + a;
	}
    public static Color LinearTween (Color ca, Color cb, float t, float d = 1f) {
        // simple linear tweening - no easing
        Vector3 c = Color2Vector(cb-ca);
        t = Mathf.Max(0,Mathf.Min(d, t));
        return Vector2Color(c*t/d) + ca;
    }

	public static float   EaseInOut (float a, float b, float t, float d = 1f) {
		float c = b-a;
		t /= d/2;//
		if (t < 1) return c/2*t*t + a;
		t--;
		return -c/2 * (t*(t-2) - 1) + a;
	}
	public static Vector3 EaseInOut (Vector3 a, Vector3 b, float t, float d = 1f) {
		Vector3 c = b-a;
		t /= d/2;
		if (t < 1) return c/2*t*t + a;
		t--;
		return -c/2 * (t*(t-2) - 1) + a;
	}

    public static Color EaseInOut(Color a1, Color b1, float t, float d = 1f)
    {
        Vector4 a = Color2Vector(a1);
        Vector4 b = Color2Vector(b1);
        Vector4 c = b - a;
        t /= d / 2;
        if (t < 1) return Vector2Color(c / 2 * t * t + a);
        t--;
        return Vector2Color(-c / 2 * (t * (t - 2) - 1) + a);
    }

    static Vector4 Color2Vector (Color c)
    {
        return new Vector4(c.r, c.g, c.b, c.a);
    }

    static Color Vector2Color(Vector4 v)
    {
        return new Color(v.x, v.y, v.z, v.w);
    }

	
	///////////// QUADRATIC EASING: t^2 ///////////////////
	/// t: current time, a: start value, b: end value, c = change value, t: elapsed time, d: duration (optional)
	public static Vector3 EaseInQuad ( Vector3 a, Vector3 b, float t, float d = 1f) {
	// quadratic easing in - accelerating from zero velocity
		Vector3 c = b-a; 	
		t /= d;
		return c*t*t + a;
	}
	public static float   EaseInQuad ( float a, float b, float t, float d = 1f) {
	// quadratic easing in - accelerating from zero velocity
		float c = b-a; 	
		t /= d;
		return c*t*t + a;
	}
	public static Vector3 EaseOutQuad (Vector3 a, Vector3 b, float t, float d = 1f) {
	// quadratic easing out - decelerating to zero velocity
		Vector3 c = b-a; 	
		t /= d;
		return -c * t*(t-2) + a;
	}
	public static float   EaseOutQuad (float a, float b, float t, float d = 1f) {
	// quadratic easing out - decelerating to zero velocity
		float c = b-a; 	
		t /= d;
		return -c * t*(t-2) + a;
	}
	public static Vector3 EaseInOutQuad ( Vector3 a, Vector3 b, float t, float d = 1f) {
	// quadratic easing in/out - acceleration until halfway, then deceleration
		Vector3 c = b-a; 	
		t /= d/2;
		if (t < 1) return c/2*t*t + a;
		t--;
		return -c/2 * (t*(t-2) - 1) + a;
	}
	public static float   EaseInOutQuad ( float a, float b, float t, float d = 1f) {
	// quadratic easing in/out - acceleration until halfway, then deceleration
		float c = b-a; 	
		t /= d/2;
		if (t < 1) return c/2*t*t + a;
		t--;
		return -c/2 * (t*(t-2) - 1) + a;
	}
		
	///////////// CUBIC EASING: t^3 ///////////////////////
	// t: current time, a: start value, b: end value, c = change value, t: elapsed time, d: duration (optional)
	public static Vector3 EaseInCubic (Vector3 a, Vector3 b, float t, float d = 1f) {
	// cubic easing in - accelerating from zero velocity
		Vector3 c = b-a; 	
		t /= d;
		return c*t*t*t + a;
	}
	public static float   EaseInCubic (float a, float b, float t, float d = 1f) {
	// cubic easing in - accelerating from zero velocity
		float c = b-a; 	
		t /= d;
		return c*t*t*t + a;
	}
	public static Vector3 EaseOutCubic (Vector3 a, Vector3 b, float t, float d = 1f) {
	// cubic easing out - decelerating to zero velocity
		Vector3 c = b-a; 	
		t /= d;
		t--;
		return c*(t*t*t + 1) + a;
	}

    public static Color EaseOutCubic (Color ca, Color cb, float t, float d = 1f) {
        // cubic easing out - decelerating to zero velocity
        Color c = Color2Vector(cb-ca);    
        t /= d;
        t--;
        return c*(t*t*t + 1) + ca;
    }


	public static float   EaseOutCubic (float a, float b, float t, float d = 1f) {
	// cubic easing out - decelerating to zero velocity
		float c = b-a; 	
		t /= d;
		t--;
		return c*(t*t*t + 1) + a;
	}

	public static Vector3 EaseInOutCubic (Vector3 a, Vector3 b, float t, float d = 1f) {
	// cubic easing in/out - acceleration until halfway, then deceleration
		Vector3 c = b-a;
		t /= d/2;
		if (t < 1) return c/2*t*t*t + a;
		t -= 2;
		return c/2*(t*t*t + 2) + a;
	}

    public static Color EaseInOutCubic (Color ca, Color cb, float t, float d = 1f) {
        // cubic easing in/out - acceleration until halfway, then deceleration
        Vector3 a = Color2Vector(ca);
        Vector3 b = Color2Vector(cb);
        Vector3 c = b-a;
        t /= d/2;
        if (t < 1) return Vector2Color(c/2*t*t*t + a);
        t -= 2;
        return Vector2Color(c/2*(t*t*t + 2) + a);
    }


	public static float   EaseInOutCubic (float a, float b, float t, float d = 1f) {
	// cubic easing in/out - acceleration until halfway, then deceleration
		float c = b-a;
		t /= d/2;
		if (t < 1) return c/2*t*t*t + a;
		t -= 2;
		return c/2*(t*t*t + 2) + a;
	}
	
	///////////// QUARTIC EASING: t^4 /////////////////////
	// t: current time, a: start value, b: end value, c = change value, t: elapsed time, d: duration (optional)
	public static Vector3 EaseInQuart (Vector3 a, Vector3 b, float t, float d = 1f) {
	// quartic easing in - accelerating from zero velocity
		Vector3 c = b-a;
		t /= d;
		return c*t*t*t*t + a;
	}
	public static float   EaseInQuart (float a, float b, float t, float d = 1f) {
	// quartic easing in - accelerating from zero velocity
		float c = b-a;
		t /= d;
		return c*t*t*t*t + a;
	}
	public static Vector3 EaseOutQuart (Vector3 a, Vector3 b, float t, float d = 1f) {
	// quartic easing out - decelerating to zero velocity
		Vector3 c = b-a;
		t /= d;
		t--;
		return -c * (t*t*t*t - 1) + a;
	}
	public static float   EaseOutQuart (float a, float b, float t, float d = 1f) {
	// quartic easing out - decelerating to zero velocity
		float c = b-a;
		t /= d;
		t--;
		return -c * (t*t*t*t - 1) + a;
	}
	public static Vector3 EaseInOutQuart ( Vector3 a, Vector3 b, float t, float d = 1f) {
	// quartic easing in/out - acceleration until halfway, then deceleration
		Vector3 c = b-a; 	
		t /= d/2;
		if (t < 1) return c/2*t*t*t*t + a;
		t -= 2;
		return -c/2 * (t*t*t*t - 2) + a;
	}
	public static float   EaseInOutQuart ( float a, float b, float t, float d = 1f) {
	// quartic easing in/out - acceleration until halfway, then deceleration
		float c = b-a; 	
		t /= d/2;
		if (t < 1) return c/2*t*t*t*t + a;
		t -= 2;
		return -c/2 * (t*t*t*t - 2) + a;
	}
	
	///////////// QUINTIC EASING: t^5  ////////////////////
	// t: current time, a: start value, b: end value, c = change value, t: elapsed time, d: duration (optional)
	public static Vector3 EaseInQuint (Vector3 a, Vector3 b, float t, float d = 1f) {
	// quintic easing in - accelerating from zero velocity
		Vector3 c = b-a;
		t /= d;
		return c*t*t*t*t*t + a;
	}
	public static float   EaseInQuint (float a, float b, float t, float d = 1f) {
	// quintic easing in - accelerating from zero velocity
		float c = b-a;
		t /= d;
		return c*t*t*t*t*t + a;
	}
	public static Vector3 EaseOutQuint (Vector3 a, Vector3 b, float t, float d = 1f) {
	// quintic easing out - decelerating to zero velocity
		Vector3 c = b-a;
		t /= d;
		t--;
		return c*(t*t*t*t*t + 1) + a;
	}
	public static float   EaseOutQuint (float a, float b, float t, float d = 1f) {
	// quintic easing out - decelerating to zero velocity
		float c = b-a;
		t /= d;
		t--;
		return c*(t*t*t*t*t + 1) + a;
	}
	public static Vector3 EaseInOutQuint ( Vector3 a, Vector3 b, float t, float d = 1f) {
	// quintic easing in/out - acceleration until halfway, then deceleration
		Vector3 c = b-a; 	
		t /= d/2;
		if (t < 1) return c/2*t*t*t*t*t + a;
		t -= 2;
		return c/2*(t*t*t*t*t + 2) + a;
	}
	public static float   EaseInOutQuint ( float a, float b, float t, float d = 1f) {
	// quintic easing in/out - acceleration until halfway, then deceleration
		float c = b-a; 	
		t /= d/2;
		if (t < 1) return c/2*t*t*t*t*t + a;
		t -= 2;
		return c/2*(t*t*t*t*t + 2) + a;
	}
	
	///////////// SINUSOIDAL EASING: sin(t) ///////////////
	// t: current time, a: start value, b: end value, c = change value, t: elapsed time, d: duration (optional)
	public static Vector3 EaseInSine (Vector3 a, Vector3 b, float t, float d = 1f) {
	// sinusoidal easing in - accelerating from zero velocity
		Vector3 c = b-a;
		return -c * Mathf.Cos(t/d * (Mathf.PI/2)) + c + a;
	}
	public static float   EaseInSine (float a, float b, float t, float d = 1f) {
	// sinusoidal easing in - accelerating from zero velocity
		float c = b-a;
		return -c * Mathf.Cos(t/d * (Mathf.PI/2)) + c + a;
	}
	public static Vector3 EaseOutSine (Vector3 a, Vector3 b, float t, float d = 1f) {
	// sinusoidal easing out - decelerating to zero velocity
		Vector3 c = b-a;
		return c * Mathf.Sin(t/d * (Mathf.PI/2)) + b;
	}
	public static float   EaseOutSine (float a, float b, float t, float d = 1f) {
	// sinusoidal easing out - decelerating to zero velocity
		float c = b-a;
		return c * Mathf.Sin(t/d * (Mathf.PI/2)) + b;
	}
	public static Vector3 EaseInOutSine (Vector3 a, Vector3 b, float t, float d = 1f) {
	// sinusoidal easing in/out - accelerating until halfway, then decelerating
		Vector3 c = b-a;
		return -c/2 * (Mathf.Cos(Mathf.PI*t/d) - 1) + a;
	}
	public static float   EaseInOutSine (float a, float b, float t, float d = 1f) {
	// sinusoidal easing in/out - accelerating until halfway, then decelerating
		float c = b-a;
		return -c/2 * (Mathf.Cos(Mathf.PI*t/d) - 1) + a;
	}
	
	///////////// EXPONENTIAL EASING: 2^t /////////////////
	// t: current time, a: start value, b: end value, c = change value, t: elapsed time, d: duration (optional)
	public static Vector3 EaseInExpo (Vector3 a, Vector3 b, float t, float d = 1f) {
	// exponential easing in - accelerating from zero velocity
		Vector3 c = b-a;
		return c * Mathf.Pow( 2, 10 * (t/d - 1) ) + a;
	}
	public static float   EaseInExpo (float a, float b, float t, float d = 1f) {
	// exponential easing in - accelerating from zero velocity
		float c = b-a;
		return c * Mathf.Pow( 2, 10 * (t/d - 1) ) + a;
	}
	public static Vector3 EaseOutExpo (Vector3 a, Vector3 b, float t, float d = 1f) {
	// exponential easing out - decelerating to zero velocity
		Vector3 c = b-a;
		return c * ( -Mathf.Pow( 2, -10 * t/d ) + 1 ) + a;
	}
	public static float   EaseOutExpo (float a, float b, float t, float d = 1f) {
	// exponential easing out - decelerating to zero velocity
		float c = b-a;
		return c * ( -Mathf.Pow( 2, -10 * t/d ) + 1 ) + a;
	}
	public static Vector3 EaseInOutExpo (Vector3 a, Vector3 b, float t, float d = 1f) {
	// exponential easing in/out - accelerating until halfway, then decelerating
		Vector3 c = b-a;
		t /= d/2;
		if (t < 1) return c/2 * Mathf.Pow( 2, 10 * (t - 1) ) + a;
		t--;
		return c/2 * ( -Mathf.Pow( 2, -10 * t) + 2 ) + a;
	}
	public static float   EaseInOutExpo (float a, float b, float t, float d = 1f) {
	// exponential easing in/out - accelerating until halfway, then decelerating
		float c = b-a;
		t /= d/2;
		if (t < 1) return c/2 * Mathf.Pow( 2, 10 * (t - 1) ) + a;
		t--;
		return c/2 * ( -Mathf.Pow( 2, -10 * t) + 2 ) + a;
	}
	
	/////////// CIRCULAR EASING: sqrt(1-t^2) //////////////
	// t: current time, a: start value, b: end value, c = change value, t: elapsed time, d: duration (optional)
	public static Vector3 EaseInCirc (Vector3 a, Vector3 b, float t, float d = 1f) {
	// circular easing in - accelerating from zero velocity
		Vector3 c = b-a;
		t /= d;
		return -c * (Mathf.Sqrt(1 - t*t) - 1) + a;
	}
	public static float   EaseInCirc (float a, float b, float t, float d = 1f) {
	// circular easing in - accelerating from zero velocity
		float c = b-a;
		t /= d;
		return -c * (Mathf.Sqrt(1 - t*t) - 1) + a;
	}	
	public static Vector3 EaseOutCirc (Vector3 a, Vector3 b, float t, float d = 1f) {
	// circular easing out - decelerating to zero velocity
		Vector3 c = b-a;
		t /= d;
		t--;
		return c * Mathf.Sqrt(1 - t*t) + a;
	}
	public static float   EaseOutCirc (float a, float b, float t, float d = 1f) {
	// circular easing out - decelerating to zero velocity
		float c = b-a;
		t /= d;
		t--;
		return c * Mathf.Sqrt(1 - t*t) + a;
	}	
	public static Vector3 EaseInOutCirc (Vector3 a, Vector3 b, float t, float d = 1f) {
	// circular easing in/out - acceleration until halfway, then deceleration
		Vector3 c = b-a;
		t /= d/2;
		if (t < 1) return -c/2 * (Mathf.Sqrt(1 - t*t) - 1) + a;
		t -= 2;
		return c/2 * (Mathf.Sqrt(1 - t*t) + 1) + a;
	}
	public static float   EaseInOutCirc (float a, float b, float t, float d = 1f) {
	// circular easing in/out - acceleration until halfway, then deceleration
		float c = b-a;
		t /= d/2;
		if (t < 1) return -c/2 * (Mathf.Sqrt(1 - t*t) - 1) + a;
		t -= 2;
		return c/2 * (Mathf.Sqrt(1 - t*t) + 1) + a;
	}
	
	/////////// ELASTIC EASING: exponentially decaying sine wave  //////////////
	// t: current time, a: start value, b: end value, c: change value, d: duration (optional), m: amplitude (optional), p: period (optional)
	public static Vector3 EaseInElastic (Vector3 a, Vector3 b, float t, float m = 1f, float p = -1f, float d = 1f ) {
		Vector3 c = b-a;
		if (t==0) return a; if ((t/=d)==1) return a+c; if (p==-1f) p=d*0.3f;
		float s;
		Vector3 mv = c.normalized*m;
		if (mv.magnitude < c.magnitude) {
			mv=c;
			s=p/4;
		} else {
			s = p/(2*Mathf.PI) * Mathf.Asin(c.magnitude/m);
		}
		return -(mv*Mathf.Pow(2,10*(t-=1)) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p )) + a;
	}
	public static float   EaseInElastic (float a, float b, float t, float m = 1f, float p = -1f, float d = 1f ) {
		float c = b-a;
		if (t==0) return a; if ((t/=d)==1) return a+c; if (p==-1f) p=d*0.3f;
		float s;
		if (m < Mathf.Abs(c)) {
			m=c;
			s=p/4;
		} else {
			s = p/(2*Mathf.PI) * Mathf.Asin(c/m);
		}
		return -(m*Mathf.Pow(2,10*(t-=1)) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p )) + a;
	}
	public static Vector3 EaseOutElastic (Vector3 a, Vector3 b, float t, float m = 1f, float p = -1f, float d = 1f ) {
		Vector3 c = b-a;
		float s;
		if (t==0) return a;  if ((t/=d)==1) return a+c;  if (p==-1f) p=d*0.3f;
		Vector3 mv = c.normalized*m;
		if (mv.magnitude < c.magnitude) {
			mv=c;
			s=p/4;
		} else {
			s = p/(2*Mathf.PI) * Mathf.Asin (c.magnitude/m);
		}
		return mv*Mathf.Pow(2,-10*t) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p ) + c + a;
	}
	public static float   EaseOutElastic (float a, float b, float t, float m = 1f, float p = -1f, float d = 1f ) {
		float c = b-a;
		float s;
		if (t==0) return a;  if ((t/=d)==1) return a+c;  if (p==-1f) p=d*0.3f;
		if (m < Mathf.Abs(c)) {
			m=c;
			s=p/4;
		} else {
			s = p/(2*Mathf.PI) * Mathf.Asin (c/m);
		}
		return m*Mathf.Pow(2,-10*t) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p ) + c + a;
	}
	public static Vector3 EaseInOutElastic (Vector3 a, Vector3 b, float t, float m = 1f, float p = -1f, float d = 1f ) {
		Vector3 c = b-a;
		if (t==0) return a;  if ((t/=d/2)==2) return a+c;  if (p==-1f) p=d*(0.3f*1.5f);
		float s;
		Vector3 mv = c.normalized*m;
		if (mv.magnitude < c.magnitude) {
			s=p/4;
		} else {
			s = p/(2*Mathf.PI) * Mathf.Asin(c.magnitude/m);
		}
		if (t < 1) return -0.5f*(mv*Mathf.Pow(2,10*(t-=1)) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p )) + a;
		return mv*Mathf.Pow(2,-10*(t-=1)) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p )*0.5f + c + a;
	}
	public static float   EaseInOutElastic (float a, float b, float t, float m = 1f, float p = -1f, float d = 1f ) {
		float c = b-a;
		if (t==0) return a;  if ((t/=d/2)==2) return a+c;  if (p==-1f) p=d*(0.3f*1.5f);
		float s;
		if (m < Mathf.Abs(c)) { 
			m=c; 
			s=p/4;
		} else {
			s = p/(2*Mathf.PI) * Mathf.Asin(c/m);
		}
		if (t < 1) return -0.5f*(m*Mathf.Pow(2,10*(t-=1)) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p )) + a;
		return m*Mathf.Pow(2,-10*(t-=1)) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p )*0.5f + c + a;
	}
	
	/////////// BACK EASING: overshooting cubic easing: (s+1)*t^3 - s*t^2  //////////////
	// t: current time, a: start value, b: end value, c: change value, d: duration  (optional), s: overshoot amount (optional)
	// s has a default value of 1.70158, which produces an overshoot of 10 percent
	// s==0 produces cubic easing with no overshoot
	public static Vector3 EaseInBack (Vector3 a, Vector3 b, float t, float s = 1.70158f, float d = 1f ) {
	// back easing in - backtracking slightly, then reversing direction and moving to target
		Vector3 c = b-a;
		return c*(t/=d)*t*((s+1)*t - s) + a;
	}
	public static float   EaseInBack (float a, float b, float t, float s = 1.70158f, float d = 1f ) {
	// back easing in - backtracking slightly, then reversing direction and moving to target
		float c = b-a;
		return c*(t/=d)*t*((s+1)*t - s) + a;
	}
	public static Vector3 EaseOutBack (Vector3 a, Vector3 b, float t, float s = 1.70158f, float d = 1f ) {
	// back easing out - moving towards target, overshooting it slightly, then reversing and coming back to target
		Vector3 c = b-a;
		return c*((t=t/d-1)*t*((s+1)*t + s) + 1) + a;
	}
	public static float   EaseOutBack (float a, float b, float t, float s = 1.70158f, float d = 1f ) {
	// back easing out - moving towards target, overshooting it slightly, then reversing and coming back to target
		float c = b-a;
		return c*((t=t/d-1)*t*((s+1)*t + s) + 1) + a;
	}
	public static Vector3 EaseInOutBack (Vector3 a, Vector3 b, float t, float s = 1.70158f, float d = 1f ) {
	// back easing in/out - backtracking slightly, then reversing direction and moving to target, then overshooting target, reversing, and finally coming back to target
		Vector3 c = b-a;
		if ((t/=d/2) < 1) return c/2*(t*t*(((s*=(1.525f))+1)*t - s)) + a;
		return c/2*((t-=2)*t*(((s*=(1.525f))+1)*t + s) + 2) + a;
	}
	public static float   EaseInOutBack (float a, float b, float t, float s = 1.70158f, float d = 1f ) {
	// back easing in/out - backtracking slightly, then reversing direction and moving to target, then overshooting target, reversing, and finally coming back to target
		float c = b-a;
		if ((t/=d/2) < 1) return c/2*(t*t*(((s*=(1.525f))+1)*t - s)) + a;
		return c/2*((t-=2)*t*(((s*=(1.525f))+1)*t + s) + 2) + a;
	}
	
	 /////////// BOUNCE EASING: exponentially decaying parabolic bounce  //////////////
	public static Vector3 EaseInBounce (Vector3 a, Vector3 b, float t, float d = 1f) {
	// bounce easing in
		Vector3 c = b-a;
		return c - EaseOutBounce ( Vector3.zero, b, d-t, d) + a;
	}
	public static float   EaseInBounce (float a, float b, float t, float d = 1f) {
	// bounce easing in
		float c = b-a;
		return c - EaseOutBounce ( 0, b, d-t, d) + a;
	}
	public static Vector3 EaseOutBounce (Vector3 a, Vector3 b, float t, float d = 1f) {
	// bounce easing out
		Vector3 c = b-a;
		if ((t/=d) < (1f/2.75f)) {
			return c*(7.5625f*t*t) + a;
		} else if (t < (2f/2.75f)) {
			return c*(7.5625f*(t-=(1.5f/2.75f))*t + 0.75f) + a;
		} else if (t < (2.5f/2.75f)) {
			return c*(7.5625f*(t-=(2.25f/2.75f))*t + 0.9375f) + a;
		} else {
			return c*(7.5625f*(t-=(2.625f/2.75f))*t + 0.984375f) + a;
		}
	}
	public static float   EaseOutBounce (float a, float b, float t, float d = 1f) {
	// bounce easing out
		float c = b-a;
		if ((t/=d) < (1f/2.75f)) {
			return c*(7.5625f*t*t) + a;
		} else if (t < (2f/2.75f)) {
			return c*(7.5625f*(t-=(1.5f/2.75f))*t + 0.75f) + a;
		} else if (t < (2.5f/2.75f)) {
			return c*(7.5625f*(t-=(2.25f/2.75f))*t + 0.9375f) + a;
		} else {
			return c*(7.5625f*(t-=(2.625f/2.75f))*t + 0.984375f) + a;
		}
	}
	public static Vector3 EaseInOutBounce (Vector3 a, Vector3 b, float t, float d = 1f) {
	// bounce easing in/out
		Vector3 c = b-a;
		if (t < d/2) return EaseInBounce (Vector3.zero, c, t*2f,d) * 0.5f + a;
		return EaseOutBounce (Vector3.zero, c, t*2-d, d) * 0.5f + c*0.5f + a;
	}
	public static float   EaseInOutBounce (float a, float b, float t, float d = 1f) {
	// bounce easing in/out
		float c = b-a;
		if (t < d/2) return EaseInBounce (0, c, t*2f,d) * 0.5f + a;
		return EaseOutBounce (0, c, t*2-d, d) * 0.5f + c*0.5f + a;
	}

}
