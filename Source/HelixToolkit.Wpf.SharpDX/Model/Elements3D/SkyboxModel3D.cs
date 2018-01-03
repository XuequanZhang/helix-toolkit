﻿using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using HelixToolkit.Wpf.SharpDX.Shaders;
using HelixToolkit.Wpf.SharpDX.Core;
using System.Windows;
using System.IO;

namespace HelixToolkit.Wpf.SharpDX
{
    public class SkyboxModel3D : Element3D
    {
        public static readonly DependencyProperty TextureProperty = DependencyProperty.Register("Texture", typeof(Stream), typeof(SkyboxModel3D),
            new PropertyMetadata(null,(d,e)=> 
            {
                ((d as Element3D).RenderCore as ISkyboxRenderParams).CubeTexture = (Stream)e.NewValue;
            }));

        public Stream Texture
        {
            set
            {
                SetValue(TextureProperty, value);
            }
            get
            {
                return (Stream)GetValue(TextureProperty);
            }
        }

        protected override IRenderCore OnCreateRenderCore()
        {
            return new SkyBoxRenderCore();
        }

        protected override void AssignDefaultValuesToCore(IRenderCore core)
        {
            base.AssignDefaultValuesToCore(core);
            (RenderCore as ISkyboxRenderParams).CubeTexture = Texture;
        }

        protected override IRenderTechnique OnCreateRenderTechnique(IRenderHost host)
        {
            return host.EffectsManager[DefaultRenderTechniqueNames.Skybox];
        }
    }
}
