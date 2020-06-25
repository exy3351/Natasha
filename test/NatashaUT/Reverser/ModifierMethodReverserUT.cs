﻿using Natasha.Reverser;
using NatashaUT.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace NatashaUT.Reverser
{

    [Trait("反解器", "方法修饰符")]
    public class ModifierMethodReverserUT
    {



        public bool IsNotNewMethod(MethodInfo info)
        {

            var baseInfo = info
                .DeclaringType
                .BaseType
                .GetMethod(info.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            return info == baseInfo;

        }

        [Fact(DisplayName = "虚方法元数据判别")]
        public void TestMethod1()
        {
            var info = typeof(VirtualMethodModel).GetMethod("M");

            Assert.True(info.IsVirtual);
            Assert.True(info.IsHideBySig);
            Assert.True(info.GetBaseDefinition() == info);
            Assert.False(info.IsAbstract);
            Assert.False(info.IsFinal);
            Assert.False(IsNotNewMethod(info));
            Assert.Equal("virtual ", Natasha.Reverser.ModifierReverser.GetModifier(info));
        }

        [Fact(DisplayName = "接口方法元数据判别")]
        public void TestMethod2()
        {
            var info = typeof(InterfaceMethodModel).GetMethod("M");

            Assert.True(info.IsVirtual);
            Assert.True(info.IsHideBySig);
            Assert.True(info.IsAbstract);
            Assert.True(info.GetBaseDefinition() == info);
            Assert.False(info.IsFinal);
            Assert.Equal("", Natasha.Reverser.ModifierReverser.GetModifier(info));
        }


        [Fact(DisplayName = "抽象方法元数据判别")]
        public void TestMethod3()
        {
            var info = typeof(AbstractMethodModel).GetMethod("M");
            Assert.True(info.IsVirtual);
            Assert.True(info.IsHideBySig);
            Assert.True(info.IsAbstract);
            Assert.True(info.GetBaseDefinition() == info);
            Assert.False(info.IsFinal);
            Assert.False(IsNotNewMethod(info));
            Assert.Equal("abstract ", Natasha.Reverser.ModifierReverser.GetModifier(info));
        }

        [Fact(DisplayName = "普通方法元数据判别")]
        public void TestMetho4()
        {

            var info = typeof(MethodModel).GetMethod("M");

            Assert.True(info.IsHideBySig);
            Assert.True(info.GetBaseDefinition() == info);

            Assert.False(info.IsVirtual);
            Assert.False(info.IsAbstract);
            Assert.False(info.IsFinal);

            Assert.False(IsNotNewMethod(info));
            Assert.Equal("", Natasha.Reverser.ModifierReverser.GetModifier(info));
        }

        [Fact(DisplayName = "重写抽象方法元数据判别")]
        public void TestMetho5()
        {

            var info = typeof(OverrideAbstractMethodModel).GetMethod("M");

            Assert.True(info.IsVirtual);
            Assert.True(info.IsHideBySig);


            Assert.False(info.GetBaseDefinition() == info);
            Assert.False(info.IsFinal);
            Assert.False(info.IsAbstract);
            Assert.False(IsNotNewMethod(info));
            Assert.Equal("override ", Natasha.Reverser.ModifierReverser.GetModifier(info));
        }

        [Fact(DisplayName = "重写虚方法元数据判别")]
        public void TestMetho6()
        {

            var info = typeof(OverrideVirtualMethodModel).GetMethod("M");

            Assert.True(info.IsVirtual);
            Assert.True(info.IsHideBySig);

            Assert.False(info.GetBaseDefinition() == info);
            Assert.False(info.IsAbstract);
            Assert.False(info.IsFinal);
            Assert.False(IsNotNewMethod(info));
            Assert.Equal("override ", Natasha.Reverser.ModifierReverser.GetModifier(info));

        }

        [Fact(DisplayName = "虚方法实现接口元数据判别")]
        public void TestMetho7()
        {

            var info = typeof(VirtualInterfaceMethodModel).GetMethod("M");

            Assert.True(info.IsVirtual);
            Assert.True(info.IsHideBySig);

            Assert.True(info.GetBaseDefinition() == info);
            Assert.False(info.IsAbstract);
            Assert.False(info.IsFinal);
            Assert.False(IsNotNewMethod(info));
            Assert.Equal("virtual ", Natasha.Reverser.ModifierReverser.GetModifier(info));

        }



        [Fact(DisplayName = "普通方法实现接口元数据判别")]
        public void TestMetho8()
        {

            var info = typeof(ImplementMethodModel).GetMethod("M");

            Assert.True(info.IsVirtual);
            Assert.True(info.IsHideBySig);
            Assert.True(info.IsFinal);

            Assert.True(info.GetBaseDefinition() == info);
            Assert.False(info.IsAbstract);
            Assert.False(IsNotNewMethod(info));
            Assert.Equal("", Natasha.Reverser.ModifierReverser.GetModifier(info));

        }

        [Fact(DisplayName = "抽象方法实现接口元数据判别")]
        public void TestMetho9()
        {

            var info = typeof(AbstractInterfaceMethodModel).GetMethod("M");

            Assert.True(info.IsVirtual);
            Assert.True(info.IsHideBySig);
            Assert.True(info.IsAbstract);

            Assert.True(info.GetBaseDefinition() == info);
            Assert.False(info.IsFinal);
            Assert.False(IsNotNewMethod(info));
            Assert.Equal("abstract ", Natasha.Reverser.ModifierReverser.GetModifier(info));
        }

        [Fact(DisplayName = "New方法元数据判别")]
        public void TestMetho10()
        {

            var info = typeof(NewMethodModel).GetMethod("M");

            Assert.True(info.IsHideBySig);

            Assert.True(info.GetBaseDefinition() == info);
            Assert.False(info.IsAbstract);
            Assert.False(info.IsVirtual);
            Assert.False(info.IsFinal);
            Assert.False(IsNotNewMethod(info));
            Assert.Equal("new ", Natasha.Reverser.ModifierReverser.GetModifier(info));

        }
    }
}
