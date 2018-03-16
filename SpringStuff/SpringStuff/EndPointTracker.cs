using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class EndPointTracker
{
    public Spring s;
    public Dictionary<Vector3, int> points = new Dictionary<Vector3, int>();
    public float index = 0;
    public EndPointTracker(Spring s, Dictionary<Vector3, int> points)
    {
        this.s = s;
        this.points = points;
    }

    public void Track()
    {
        List<Vector3> toDelete = new List<Vector3>();
        foreach (Vector3 p in points.Keys.ToList().OrderByDescending(x => x.Z))
        {            
            points[p]++;
            if (points[p] > 500) toDelete.Add(p);
        }
        foreach (Vector3 d in toDelete)
            points.Remove(d);        
        points.Add(new Vector3((float)s.endPointX, (float)s.endPointY, index), 0);
        index++;
    }

}

