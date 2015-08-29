#define uniformTexture(texture) uniform sampler2D texture##_; uniform mat3 texture##Matrix_;
#define texture(texture, pos) texture2D(texture##_, (texture##Matrix_ * vec3(pos, 1)).xy)
