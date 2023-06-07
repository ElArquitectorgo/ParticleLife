import Color

class Particle():
    def __init__(self, x: int, y: int, color: Color):
        self.x = x
        self.y = y
        self.vel_x = 0
        self.vel_y = 0
        self.color = color
            
