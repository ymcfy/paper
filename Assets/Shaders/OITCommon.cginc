﻿/*
MIT License

Copyright (c) 2018 Pantelis Lekakis

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#ifndef OITCOMMON_CGINC
#define OITCOMMON_CGINC

#define MAX_OIT_PIXEL_NODES (20u)	// This is the maximum depth of per-pixel nodes we'll process per pixel

// Represents a pixel, its depth and its next link
struct OITNode
{
	float4	color;
	float	depth;
	uint	next;
};

RWStructuredBuffer<uint>	_OITHeadPointers : register(u1);		// a [Width x Height] sized buffer pointing to the right index in the node buffer (below)
RWStructuredBuffer<OITNode> _OITNodes : register(u2);				// a [Width x Heigth x MAX_OIT_PIXEL_NODES] sized buffer containing a linked list of color & depth information per pixel

#endif // OITCOMMONG_CGINC