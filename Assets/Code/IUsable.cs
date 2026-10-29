using UnityEngine;
using UnityEngine.InputSystem;

namespace Wp.Items {
    public interface IUsable {
        public void Use(WpItemUseContext context);
    }
}
