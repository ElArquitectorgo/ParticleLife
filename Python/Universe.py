
class Universe:
    def __init__(self, width, rules):
        self.width = width
        self.rules = rules
        self.attraction_range = 100

    def update(self, particles):

        for particle_a in particles:
            fx = 0
            fy = 0
            for particle_b in particles:
                dx = particle_b.x - particle_a.x
                dy = particle_b.y - particle_a.y

                if dx > self.width - self.attraction_range: dx -= self.width
                elif dx < -self.width + self.attraction_range: dx += self.width
                if dy > self.width - self.attraction_range: dy -= self.width
                elif dy < -self.width + self.attraction_range: dy += self.width

                # Manhattan distance
                distance = abs(dx) + abs(dy)

                if distance > 0 and distance < self.attraction_range:
                    gravitationalConstant = self.rules.rules[particle_a.color, particle_b.color]
                    f = distance / (-5) if (distance < 30) else gravitationalConstant * (1 - abs(15 - 2 * distance) / 15)
                    Force = f / (distance * distance)

                    fx += Force * dx
                    fy += Force * dy

            particle_a.vel_x = (particle_a.vel_x + fx) * 0.5;
            particle_a.vel_y = (particle_a.vel_y + fy) * 0.5;

            particle_a.x += particle_a.vel_x;
            particle_a.y += particle_a.vel_y;

            if particle_a.x < 0: particle_a.x = self.width
            if particle_a.x > self.width: particle_a.x = 0
            if particle_a.y < 0: particle_a.y = self.width
            if particle_a.y > self.width: particle_a.y = 0