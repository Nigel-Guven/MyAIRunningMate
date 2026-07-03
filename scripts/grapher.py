import json
import networkx as nx
from collections import Counter

FILE = "phoenix_park_routes.json"

def load_graph(file_path):
    with open(file_path, "r") as f:
        data = json.load(f)
    return data

def build_graph(edges):
    G = nx.Graph()  # undirected graph

    for e in edges:
        a = e["from"]
        b = e["to"]
        d = e["distance_m"]

        G.add_edge(a, b, weight=d)

    return G

def validate_graph(G):
    print("\n--- GRAPH VALIDATION ---")

    # 1. Connectivity
    connected = nx.is_connected(G)
    print(f"Connected: {connected}")

    # 2. Node degree analysis
    degrees = dict(G.degree())
    print("\nNode degrees:")
    for node, deg in sorted(degrees.items()):
        print(f"{node}: {deg}")

    # 3. Odd degree nodes (important for route optimisation)
    odd = [n for n, d in degrees.items() if d % 2 == 1]
    print(f"\nOdd degree nodes ({len(odd)}):")
    for n in odd:
        print(f" - {n}")

    # 4. Total edges + distance
    total_dist = sum(d["weight"] for *_, d in G.edges(data=True))
    print(f"\nTotal edges: {G.number_of_edges()}")
    print(f"Total distance (all edges once): {total_dist} m")

    return odd

def main():
    data = load_graph(FILE)
    G = build_graph(data["edges"])

    print(f"Loaded {len(G.nodes)} nodes")
    print(f"Loaded {len(G.edges)} edges")

    odd_nodes = validate_graph(G)

    print("\n--- NEXT STEP INSIGHT ---")
    if len(odd_nodes) == 0:
        print("Graph is Eulerian → you can traverse every edge without repeats.")
    else:
        print(f"Graph is NOT Eulerian → minimum repeats required.")
        print(f"Odd nodes to fix: {len(odd_nodes)}")

if __name__ == "__main__":
    main()