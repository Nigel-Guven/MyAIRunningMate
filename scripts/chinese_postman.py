import json
import networkx as nx
from itertools import combinations

FILE = "phoenix_park_routes.json"

def load_graph():
    with open(FILE, "r") as f:
        return json.load(f)

def build_graph(edges):
    G = nx.Graph()
    for e in edges:
        G.add_edge(e["from"], e["to"], weight=e["distance_m"])
    return G

def chinese_postman(G):
    print("\n--- CHINESE POSTMAN SOLVER ---")

    # Step 1: find odd nodes
    odd_nodes = [n for n, d in G.degree() if d % 2 == 1]
    print(f"Odd nodes: {len(odd_nodes)}")

    # Step 2: shortest paths between all odd nodes
    dist = dict(nx.all_pairs_dijkstra_path_length(G, weight="weight"))

    # Step 3: build complete graph of odd nodes
    O = nx.Graph()
    for u, v in combinations(odd_nodes, 2):
        O.add_edge(u, v, weight=dist[u][v])

    # Step 4: minimum weight perfect matching
    matching = nx.algorithms.matching.min_weight_matching(O, weight="weight")

    print(f"Pairs added: {len(matching)}")

    # Step 5: duplicate paths in original graph
    multi = nx.MultiGraph(G)

    for u, v in matching:
        path = nx.shortest_path(G, u, v, weight="weight")

        # add duplicate edges along path
        for i in range(len(path) - 1):
            a, b = path[i], path[i+1]
            w = G[a][b]["weight"]
            multi.add_edge(a, b, weight=w)

    # Step 6: Euler circuit
    circuit = list(nx.eulerian_circuit(multi))

    return circuit
    
def route_distance(circuit, G):
    total = 0
    for u, v in circuit:
        total += G[u][v]["weight"]
    return total

def main():
    data = load_graph()
    G = build_graph(data["edges"])

    print(f"Nodes: {len(G.nodes)}")
    print(f"Edges: {len(G.edges)}")

    circuit = chinese_postman(G)

    print("\n--- OPTIMAL ROUTE (EDGE WALK) ---")
    for i, (u, v) in enumerate(circuit[:50]):  # print first 50 steps
        print(f"{i+1}: {u} -> {v}")

    print("\nRoute length:", len(circuit))
    
    distance_m = route_distance(circuit, G)

    print("\nRoute stats:")
    print("Edges traversed:", len(circuit))
    print("Total distance (m):", distance_m)
    print("Total distance (km):", round(distance_m / 1000, 2))

if __name__ == "__main__":
    main()